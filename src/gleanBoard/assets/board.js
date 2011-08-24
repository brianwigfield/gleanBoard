(function() {
  window.card = function(id, name, price) {
    return {
      Id: id,
      Name: name
    };
  };
  window.lane = function(id, name, cards) {
    return {
      Id: id,
      Name: name,
      Cards: ko.observableArray(cards)
    };
  };
  window.moveById = function(ids, fromArray, toArray) {
    var moved, moving, _i, _len, _results;
    moved = fromArray.remove(function(p) {
      return ko.utils.arrayIndexOf(ids, p.Id.toString()) >= 0;
    });
    _results = [];
    for (_i = 0, _len = moved.length; _i < _len; _i++) {
      moving = moved[_i];
      _results.push(toArray.push(moving));
    }
    return _results;
  };
  window.viewModel = {
    owner: "Brian",
    lanes: ko.observableArray([]),
    newCards: ko.observableArray([new card("", "Testing")]),
    newCardTitle: ko.observable("New Card"),
    newLaneName: ko.observable("New Lane"),
    addCard: function() {
      return $.post("/card/create", {
        board: viewModel.board,
        title: viewModel.newCardTitle,
        lane: $("#newCardLane").val(),
        position: 1
      }, function(data) {
        return viewModel.findLaneById(data.Lane).Cards.push(new card(data.Id, data.Title));
      });
    },
    addLane: function() {
      return $.post("/lane/create", {
        name: viewModel.newLaneName,
        board: viewModel.board,
        position: 0
      }, function(data) {
        return viewModel.lanes.splice(0, 0, new lane(data.Id, data.Name, []));
      });
    },
    moveCard: function(ids, from, to, position) {
      var fromLane, l, lane, post, toLane;
      l = (function() {
        var _i, _len, _ref, _results;
        _ref = this.lanes();
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          lane = _ref[_i];
          if (lane.Id === from) {
            fromLane = lane;
          }
          _results.push(lane.Id === to ? toLane = lane : void 0);
        }
        return _results;
      }).call(this);
      moveById(ids, fromLane.Cards, toLane.Cards);
      post = $.post("/card/move", {
        board: viewModel.board,
        card: ids[0],
        from: from,
        to: to,
        positon: position
      }, function(data) {});
      return post.error(function() {
        return alert("error occured");
      });
    },
    createLanes: function() {
      return $(".connectedSortable").sortable({
        connectWith: ".connectedSortable",
        placeholder: "laneCardDrop",
        dropOnEmpty: true
      }).disableSelection();
    },
    findLaneById: function(id) {
      var lane;
      return ((function() {
        var _i, _len, _ref, _results;
        _ref = this.lanes();
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          lane = _ref[_i];
          if (lane.Id === id) {
            _results.push(lane);
          }
        }
        return _results;
      }).call(this))[0];
    }
  };
  ko.bindingHandlers.onReceiveItem = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel) {
      var callback;
      callback = valueAccessor();
      return $(element).bind("sortreceive", function(event, ui) {
        var receivedIds;
        receivedIds = $.map(ui.item, function(item) {
          return $(item).attr("data-id");
        });
        return callback.call(viewModel, receivedIds, ui.sender.context.id, event.target.id, ui.item.index());
      });
    }
  };
  $(function() {
    $("#addCardLink").click(function() {
      $("#cardForm").toggle();
    });
    $("#addLaneLink").click(function() {
      $("#laneForm").toggle();
    });
    return ko.applyBindings(viewModel);
  });
}).call(this);
