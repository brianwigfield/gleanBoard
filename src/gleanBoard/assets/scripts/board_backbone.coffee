$ ->
	class Card extends Backbone.Model
		defaults: 
			Id: 0, Title: '', Description: ''

	class Lane extends Backbone.Collection
		model: Card

	class LaneModel extends Backbone.Model
		defaults:
			Lane: new Lane()

	class Board extends Backbone.Collection
		model: LaneModel

	class BoardView extends Backbone.View
		el: '#board'
		initialize: ->
			this.laneTemplate = _.template $('#laneTemplate').html()
			this.cardTemplate = _.template $('#cardTemplate').html()
		render: ->
			$(this.el).html this.laneTemplate({
				lanes: this.collection.models
				cardTemplate: this.cardTemplate
			})

			$("ul.connectedSortable").sortable
				connectWith: "ul.connectedSortable",
				placeholder: "laneCardDrop",
				dropOnEmpty: true,
				revert: 500
			.disableSelection()

			setSize = ->
				$("div.boardLaneContainer").css("min-height", $(window).height() - $("div.boardLaneContainer").offset().top)
    
			$(window).resize ->
				setSize()

			setSize()

	class NewLaneView extends Backbone.View
		el: '#laneForm'
		initialize: ->
			this.lanePositionTemplate = _.template $('#newLanePositonsTemplate').html()
			$("#addLaneLink").click ->
				$("#laneForm").slideToggle()
		render: ->
			$(this.el).find("#newLanePosition").html this.lanePositionTemplate(lanes: this.options.lanes.models)
		events: 
			'click #createNewLane': 'createLane'
		createLane: ->
			$.post "/lane/create",
				Board: this.options.boardId, Name: this.$('#newLaneName').val(), Position: this.$("#newLanePosition").val(),
				(data) => 
					this.options.lanes.models.splice(data.Position, 0, new LaneModel({Name: data.Id, Name: data.Name, Lane: new Lane()}))
					this.options.boardView.render()

	class NewCardView extends Backbone.View
		el: "#cardForm"
		initialize: ->
			this.cardTemplate = _.template $('#cardTemplate').html()
			this.blankCard = new Card({Title:"blank", Description : "Testing"})
			$("addCardLink").click ->
				$(this).slideToggle()
		events: ->
			'click #createNewCard': 'createCard'
			'keyup input' : 'updateCardFromForm'
		renderCard: (card) ->
			$(this.el).find("#newCardVisualizer").html this.cardTemplate({card : card})
		updateCardFromForm: (e) ->
			this.renderCard new Card({Title: $("#newcardTitle").val(), Description: $("#newcardDescription").val()})
		render: ->
			this.renderCard this.blankCard
		createCard: ->
			$.post "/card/create",
				Board: this.options.boardId, Title: boardModel.newCards()[0].Title, Lane: onLane, Position: position, Description: boardModel.newCards()[0].Description,
				(data) => 
					$("#NewCardTemplate").remove()
					boardModel.findLaneById(data.Lane).Cards.splice data.Position, 0, new card(data.Id, data.Title, data.Description)
					boardModel.newCards.splice 0, 1, new card("NewCardTemplate", ko.observable(""), ko.observable(""))

	window.gleanBoard = {}
	window.gleanBoard.setupBoard = (id, lanes) ->
		laneModels = _.map(lanes, (lane) -> 
			new LaneModel {Name: lane.Name, Lane: new Lane(_.map(lane.Cards, (card) -> 
				new Card(card)
			))})
		col = new Board laneModels
		boardView = new BoardView id: id, collection: col
		boardView.render()
		newLane = new NewLaneView boardId: id, lanes: col, boardView: boardView
		newLane.render()
		newCard = new NewCardView boardId: id, boardView: boardView
		newCard.render()

	$("#addCardLink").click ->
		$("#cardForm").slideToggle()

	$("div.boardLaneContainer ul.boardLane li.laneCard").live 'click', ->
		$("#board").fadeTo 500, .3
		$("#editCardForm").slideDown()

	$("input[data-watermark]")
		.each ->
			$(this).val $(this).data("watermark")
			$(this).addClass "watermarked"
		.focus ->
			if $(this).val() == $(this).data("watermark")
				$(this).val ""
				$(this).removeClass "watermarked"
		.blur ->
			if $(this).val() == ""
				$(this).val $(this).data("watermark") 
				$(this).addClass "watermarked"
	