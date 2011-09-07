window.card = (id, title, description) ->
    Id: id, Title: title, Description: description

window.lane = (id, name, cards) -> 
    Id: id, Name: name, Cards: ko.observableArray(cards)

window.moveById = (id, fromArray, toArray, position) ->
    moved = fromArray.remove (p) ->
        id == p.Id.toString()
    toArray.splice position, 0, moved[0]
    
window.viewModel = 
    owner: "Brian",
    lanes: ko.observableArray([])
    newCards: ko.observableArray([
        new card "", ko.observable("Testing"), ko.observable("Description")
    ])
    newLaneName: ko.observable("New Lane")
    createCard: (onLane, position) ->
        $.post "/card/create",
               Board: viewModel.board, Title: viewModel.newCards()[0].Title, Lane: onLane, Position: position, Description: viewModel.newCards()[0].Description,
               (data) -> 
                    viewModel.findLaneById(data.Lane).Cards.push(new card(data.Id, data.Title))
    
    addLane: ->
        $.post "/lane/create",
               { name: viewModel.newLaneName, board: viewModel.board, position: 0 },
               (data) -> viewModel.lanes.splice(0, 0, new lane(data.Id, data.Name, []))
    
    moveCard: (id, from, to, position) ->
        l = for lane in viewModel.lanes()
            fromLane = lane if lane.Id == from
            toLane = lane if lane.Id == to

        ###undefined Should not be needed!###
        return viewModel.createCard to, position if not fromLane? || typeof fromLane == "undefined" 

        moveById id, fromLane.Cards, toLane.Cards, position

        post = $.post "/card/move",
               board: viewModel.board, card: id, from: from, to: to, position: position,
               (data) -> return

        post.error -> alert "error occured"

    createLanes: ->
        $(".connectedSortable").sortable(
            connectWith: ".connectedSortable",
            placeholder: "laneCardDrop",
            dropOnEmpty: true
        ).disableSelection()

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
            currentLane = window.viewModel.findLaneWithCard(receivedId).Id
            return if not ui.sender? and ui.item.context.parentNode.id != currentLane

            callback.call(viewModel, receivedId, currentLane, event.target.id, ui.item.index())

$ ->
    $("#addCardLink").click ->
        $("#cardForm").toggle()
        return

    $("#addLaneLink").click ->
        $("#laneForm").toggle()
        return

    $("div.boardLaneContainer").css "min-height", $(window).height() - $("div.boardLaneContainer").offset().top
    $(window).resize ->
        $("div.boardLaneContainer").css "min-height", $(window).height() - $("div.boardLaneContainer").offset().top

    ko.applyBindings(viewModel)