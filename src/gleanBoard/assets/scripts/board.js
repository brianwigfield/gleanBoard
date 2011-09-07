(function() {
  window.card = function(id, title, description) {
    return {
      Id: id,
      Title: title,
      Description: description
    };
  };
  window.lane = function(id, name, cards) {
    return {
      Id: id,
      Name: name,
      Cards: ko.observableArray(cards)
    };
  };
  window.moveById = function(id, fromArray, toArray, position) {
    var moved;
    moved = fromArray.remove(function(p) {
      return id === p.Id.toString();
    });
    return toArray.splice(position, 0, moved[0]);
  };
  window.viewModel = {
    owner: "Brian",
    lanes: ko.observableArray([]),
    newCards: ko.observableArray([new card("", ko.observable("Testing"), ko.observable("Description"))]),
    newLaneName: ko.observable("New Lane"),
    createCard: function(onLane, position) {
      return $.post("/card/create", {
        Board: viewModel.board,
        Title: viewModel.newCards()[0].Title,
        Lane: onLane,
        Position: position,
        Description: viewModel.newCards()[0].Description
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
    moveCard: function(id, from, to, position) {
      var fromLane, l, lane, post, toLane;
      l = (function() {
        var _i, _len, _ref, _results;
        _ref = viewModel.lanes();
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          lane = _ref[_i];
          if (lane.Id === from) {
            fromLane = lane;
          }
          _results.push(lane.Id === to ? toLane = lane : void 0);
        }
        return _results;
      })();
      /*undefined Should not be needed!*/
      if (!(fromLane != null) || typeof fromLane === "undefined") {
        return viewModel.createCard(to, position);
      }
      moveById(id, fromLane.Cards, toLane.Cards, position);
      post = $.post("/card/move", {
        board: viewModel.board,
        card: id,
        from: from,
        to: to,
        position: position
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
    },
    findLaneWithCard: function(cardId) {
      var card, lane, _i, _j, _len, _len2, _ref, _ref2, _results;
      _ref = this.lanes();
      _results = [];
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        lane = _ref[_i];
        _ref2 = lane.Cards();
        for (_j = 0, _len2 = _ref2.length; _j < _len2; _j++) {
          card = _ref2[_j];
          if (card.Id === cardId) {
            return lane;
          }
        }
      }
      return _results;
    }
  };
  ko.bindingHandlers.onCardMove = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel) {
      var callback;
      callback = valueAccessor();
      return $(element).bind("sortupdate", function(event, ui) {
        var currentLane, receivedId;
        receivedId = $(ui.item).attr("id");
        /*filter out sortupdate event fired from source lane when card moves lanes*/
        currentLane = window.viewModel.findLaneWithCard(receivedId).Id;
        if (!(ui.sender != null) && ui.item.context.parentNode.id !== currentLane) {
          return;
        }
        return callback.call(viewModel, receivedId, currentLane, event.target.id, ui.item.index());
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
    $("div.boardLaneContainer").css("min-height", $(window).height() - $("div.boardLaneContainer").offset().top);
    $(window).resize(function() {
      return $("div.boardLaneContainer").css("min-height", $(window).height() - $("div.boardLaneContainer").offset().top);
    });
    return ko.applyBindings(viewModel);
  });
}).call(this);
