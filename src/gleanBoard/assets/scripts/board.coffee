window.card = (id, title, description) ->
    Id: id, Title: title, Description: description

window.lane = (id, name, cards) -> 
    Id: id, Name: name, Cards: ko.observableArray(cards)

window.boardModel = 
    owner: "Brian"
    lanes: ko.observableArray([])
    newCards: ko.observableArray([ new card("NewCardTemplate", ko.observable(""), ko.observable("")) ])
    newLaneName: ko.observable("")
    setupLanes: (lanesToSetup) ->
        for lane in lanesToSetup
            this.lanes.push lane

    createCard: (onLane, position) ->
        $.post "/card/create",
            Board: boardModel.board, Title: boardModel.newCards()[0].Title, Lane: onLane, Position: position, Description: boardModel.newCards()[0].Description,
            (data) -> 
                $("#NewCardTemplate").remove()
                boardModel.findLaneById(data.Lane).Cards.splice data.Position, 0, new card(data.Id, data.Title, data.Description)
                boardModel.newCards.splice 0, 1, new card("NewCardTemplate", ko.observable(""), ko.observable(""))
                boardModel.newCards.splice 0, 1, new card "NewCardTemplate", ko.observable("") , ko.observable ""
    
    createLane: ->
        $.post "/lane/create",
            name: boardModel.newLaneName, board: boardModel.board, position: $("#newLanePosition").val(),
            (data) -> 
                boardModel.lanes.splice(data.Position, 0, new lane(data.Id, data.Name, []))
    
    moveCard: (id, from, to, position) ->
        for lane in boardModel.lanes()
            fromLane = lane if lane.Id == from
            toLane = lane if lane.Id == to

        return boardModel.createCard(to, position) if not fromLane? || typeof fromLane == "undefined" 

        moved = fromLane.Cards.remove (p) ->
            id == p.Id.toString()
        toLane.Cards.splice position, 0, moved[0]

        post = $.post "/card/move",
               board: boardModel.board, card: id, from: from, to: to, position: position,
               (data) -> return

        post.error -> alert "error occured"

    initBoard: ->
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

    findLaneById: (id) ->
        (lane for lane in this.lanes() when lane.Id == id)[0]

    findLaneWithCard: (cardId) ->
        for lane in this.lanes()
            for card in lane.Cards()
                return lane if card.Id == cardId
    

ko.bindingHandlers.onCardMove =
    init: (element, valueAccessor, allBindingsAccessor, viewModel) ->
        callback = valueAccessor()
        $(element).bind "sortupdate", (event, ui) ->
            receivedId = $(ui.item).attr "id"
            
            ###filter out sortupdate event fired from source lane when card moves lanes###
            currentLane = boardModel.findLaneWithCard(receivedId).Id
            return if not ui.sender? and ui.item.context.parentNode.id != currentLane

            callback.call(viewModel, receivedId, currentLane, event.target.id, ui.item.index())

$ ->
    $("#addCardLink").click ->
        $("#cardForm").slideToggle()

    $("#addLaneLink").click ->
        $("#laneForm").slideToggle()

    $("div.boardLaneContainer ul.boardLane li.laneCard").live 'click', ->
        $("#board").fadeTo 500, .3
        $("#editCardForm").slideDown()

    ko.applyBindings(boardModel)

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