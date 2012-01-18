(function() {
  var __hasProp = Object.prototype.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor; child.__super__ = parent.prototype; return child; };

  $(function() {
    var Board, BoardView, Card, Lane, LaneModel, NewCardView, NewLaneView;
    Card = (function(_super) {

      __extends(Card, _super);

      function Card() {
        Card.__super__.constructor.apply(this, arguments);
      }

      Card.prototype.defaults = {
        Id: 0,
        Title: '',
        Description: ''
      };

      return Card;

    })(Backbone.Model);
    Lane = (function(_super) {

      __extends(Lane, _super);

      function Lane() {
        Lane.__super__.constructor.apply(this, arguments);
      }

      Lane.prototype.model = Card;

      return Lane;

    })(Backbone.Collection);
    LaneModel = (function(_super) {

      __extends(LaneModel, _super);

      function LaneModel() {
        LaneModel.__super__.constructor.apply(this, arguments);
      }

      LaneModel.prototype.defaults = {
        Lane: new Lane()
      };

      return LaneModel;

    })(Backbone.Model);
    Board = (function(_super) {

      __extends(Board, _super);

      function Board() {
        Board.__super__.constructor.apply(this, arguments);
      }

      Board.prototype.model = LaneModel;

      return Board;

    })(Backbone.Collection);
    BoardView = (function(_super) {

      __extends(BoardView, _super);

      function BoardView() {
        BoardView.__super__.constructor.apply(this, arguments);
      }

      BoardView.prototype.el = '#board';

      BoardView.prototype.initialize = function() {
        this.laneTemplate = _.template($('#laneTemplate').html());
        return this.cardTemplate = _.template($('#cardTemplate').html());
      };

      BoardView.prototype.render = function() {
        var setSize;
        $(this.el).html(this.laneTemplate({
          lanes: this.collection.models,
          cardTemplate: this.cardTemplate
        }));
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
      };

      return BoardView;

    })(Backbone.View);
    NewLaneView = (function(_super) {

      __extends(NewLaneView, _super);

      function NewLaneView() {
        NewLaneView.__super__.constructor.apply(this, arguments);
      }

      NewLaneView.prototype.el = '#laneForm';

      NewLaneView.prototype.initialize = function() {
        this.lanePositionTemplate = _.template($('#newLanePositonsTemplate').html());
        return $("#addLaneLink").click(function() {
          return $("#laneForm").slideToggle();
        });
      };

      NewLaneView.prototype.render = function() {
        return $(this.el).find("#newLanePosition").html(this.lanePositionTemplate({
          lanes: this.options.lanes.models
        }));
      };

      NewLaneView.prototype.events = {
        'click #createNewLane': 'createLane'
      };

      NewLaneView.prototype.createLane = function() {
        var _this = this;
        return $.post("/lane/create", {
          Board: this.options.boardId,
          Name: this.$('#newLaneName').val(),
          Position: this.$("#newLanePosition").val()
        }, function(data) {
          _this.options.lanes.models.splice(data.Position, 0, new LaneModel({
            Name: data.Id,
            Name: data.Name,
            Lane: new Lane()
          }));
          return _this.options.boardView.render();
        });
      };

      return NewLaneView;

    })(Backbone.View);
    NewCardView = (function(_super) {

      __extends(NewCardView, _super);

      function NewCardView() {
        NewCardView.__super__.constructor.apply(this, arguments);
      }

      NewCardView.prototype.el = "#cardForm";

      NewCardView.prototype.initialize = function() {
        this.cardTemplate = _.template($('#cardTemplate').html());
        this.blankCard = new Card({
          Title: "blank",
          Description: "Testing"
        });
        return $("addCardLink").click(function() {
          return $(this).slideToggle();
        });
      };

      NewCardView.prototype.events = function() {
        return {
          'click #createNewCard': 'createCard',
          'keyup input': 'updateCardFromForm'
        };
      };

      NewCardView.prototype.renderCard = function(card) {
        return $(this.el).find("#newCardVisualizer").html(this.cardTemplate({
          card: card
        }));
      };

      NewCardView.prototype.updateCardFromForm = function(e) {
        return this.renderCard(new Card({
          Title: $("#newcardTitle").val(),
          Description: $("#newcardDescription").val()
        }));
      };

      NewCardView.prototype.render = function() {
        return this.renderCard(this.blankCard);
      };

      NewCardView.prototype.createCard = function() {
        var _this = this;
        return $.post("/card/create", {
          Board: this.options.boardId,
          Title: boardModel.newCards()[0].Title,
          Lane: onLane,
          Position: position,
          Description: boardModel.newCards()[0].Description
        }, function(data) {
          $("#NewCardTemplate").remove();
          boardModel.findLaneById(data.Lane).Cards.splice(data.Position, 0, new card(data.Id, data.Title, data.Description));
          return boardModel.newCards.splice(0, 1, new card("NewCardTemplate", ko.observable(""), ko.observable("")));
        });
      };

      return NewCardView;

    })(Backbone.View);
    window.gleanBoard = {};
    window.gleanBoard.setupBoard = function(id, lanes) {
      var boardView, col, laneModels, newCard, newLane;
      laneModels = _.map(lanes, function(lane) {
        return new LaneModel({
          Name: lane.Name,
          Lane: new Lane(_.map(lane.Cards, function(card) {
            return new Card(card);
          }))
        });
      });
      col = new Board(laneModels);
      boardView = new BoardView({
        id: id,
        collection: col
      });
      boardView.render();
      newLane = new NewLaneView({
        boardId: id,
        lanes: col,
        boardView: boardView
      });
      newLane.render();
      newCard = new NewCardView({
        boardId: id,
        boardView: boardView
      });
      return newCard.render();
    };
    $("#addCardLink").click(function() {
      return $("#cardForm").slideToggle();
    });
    $("div.boardLaneContainer ul.boardLane li.laneCard").live('click', function() {
      $("#board").fadeTo(500, .3);
      return $("#editCardForm").slideDown();
    });
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
