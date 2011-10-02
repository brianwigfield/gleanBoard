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
  window.boardModel = {
    owner: "Brian",
    lanes: ko.observableArray([]),
    newCards: ko.observableArray([new card("NewCardTemplate", ko.observable(""), ko.observable(""))]),
    newLaneName: ko.observable(""),
    setupLanes: function(lanesToSetup) {
      var lane, _i, _len, _results;
      _results = [];
      for (_i = 0, _len = lanesToSetup.length; _i < _len; _i++) {
        lane = lanesToSetup[_i];
        _results.push(this.lanes.push(lane));
      }
      return _results;
    },
    createCard: function(onLane, position) {
      return $.post("/card/create", {
        Board: boardModel.board,
        Title: boardModel.newCards()[0].Title,
        Lane: onLane,
        Position: position,
        Description: boardModel.newCards()[0].Description
      }, function(data) {
        $("#NewCardTemplate").remove();
        boardModel.findLaneById(data.Lane).Cards.splice(data.Position, 0, new card(data.Id, data.Title, data.Description));
        boardModel.newCards.splice(0, 1, new card("NewCardTemplate", ko.observable(""), ko.observable("")));
        return boardModel.newCards.splice(0, 1, new card("NewCardTemplate", ko.observable(""), ko.observable("")));
      });
    },
    createLane: function() {
      return $.post("/lane/create", {
        name: boardModel.newLaneName,
        board: boardModel.board,
        position: $("#newLanePosition").val()
      }, function(data) {
        return boardModel.lanes.splice(data.Position, 0, new lane(data.Id, data.Name, []));
      });
    },
    moveCard: function(id, from, to, position) {
      var fromLane, lane, moved, post, toLane, _i, _len, _ref;
      _ref = boardModel.lanes();
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        lane = _ref[_i];
        if (lane.Id === from) {
          fromLane = lane;
        }
        if (lane.Id === to) {
          toLane = lane;
        }
      }
      if (!(fromLane != null) || typeof fromLane === "undefined") {
        return boardModel.createCard(to, position);
      }
      moved = fromLane.Cards.remove(function(p) {
        return id === p.Id.toString();
      });
      toLane.Cards.splice(position, 0, moved[0]);
      post = $.post("/card/move", {
        board: boardModel.board,
        card: id,
        from: from,
        to: to,
        position: position
      }, function(data) {});
      return post.error(function() {
        return alert("error occured");
      });
    },
    initBoard: function() {
      var setSize;
      $("ul.connectedSortable").sortable({
        connectWith: "ul.connectedSortable",
        placeholder: "laneCardDrop",
        dropOnEmpty: true,
        revert: 500
      }).disableSelection();
      setSize = function() {
        return $("div.boardLaneContainer").css("min-height", $(window).height() - $("div.boardLaneContainer").offset().top);
      };
      $(window).resize(function() {
        return setSize();
      });
      return setSize();
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
        currentLane = boardModel.findLaneWithCard(receivedId).Id;
        if (!(ui.sender != null) && ui.item.context.parentNode.id !== currentLane) {
          return;
        }
        return callback.call(viewModel, receivedId, currentLane, event.target.id, ui.item.index());
      });
    }
  };
  $(function() {
    $("#addCardLink").click(function() {
      return $("#cardForm").slideToggle();
    });
    $("#addLaneLink").click(function() {
      return $("#laneForm").slideToggle();
    });
    $("div.boardLaneContainer ul.boardLane li.laneCard").live('click', function() {
      $("#board").fadeTo(500, .3);
      return $("#editCardForm").slideDown();
    });
    ko.applyBindings(boardModel);
    return $("input[data-watermark]").each(function() {
      $(this).val($(this).data("watermark"));
      return $(this).addClass("watermarked");
    }).focus(function() {
      if ($(this).val() === $(this).data("watermark")) {
        $(this).val("");
        return $(this).removeClass("watermarked");
      }
    }).blur(function() {
      if ($(this).val() === "") {
        $(this).val($(this).data("watermark"));
        return $(this).addClass("watermarked");
      }
    });
  });
}).call(this);
