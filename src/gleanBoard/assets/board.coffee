window.card = (id, name, price) ->  
    Id: id, Name: name

window.lane = (id, name, cards) -> 
    Id: id, Name: name, Cards: ko.observableArray(cards)

window.moveById = (ids, fromArray, toArray) ->
    moved = fromArray.remove (p) ->
        ko.utils.arrayIndexOf(ids, p.Id.toString()) >= 0
    toArray.push moving for moving in moved


window.viewModel = 
    owner: "Brian",
    lanes: ko.observableArray([]),
    newCards: ko.observableArray([
        new card "", "Testing"
    ]),
    events: ko.observableArray(),
    newCardTitle: ko.observable("New Card"),
    newLaneName: ko.observable("New Lane"),
    addCard: ->
        $.post "/card/create",
               { board: viewModel.board, title: viewModel.newCardTitle, lane: $("#newCardLane").val(), position:1 },
               (data) -> viewModel.findLaneById(data.Id).Cards.push(new card(data.Id, data.Title))
    ,
    addLane: ->
        $.post "/lane/create",
               { name: viewModel.newLaneName },
               (data) -> viewModel.lanes.push(new lane(data.Id, data.Name, []))
    ,
    moveLanes: (ids, from, to, position) ->
        this.events.push { id: ids, from: from, to: to }

        l = for lane in this.lanes()
            fromLane = lane if lane.Id == from
            toLane = lane if lane.Id == to

        moveById ids, fromLane.Cards, toLane.Cards

        post = $.post "/card/move",
               board: viewModel.board, card: ids[0], from: from, to: to, positon: position,
               (data) -> return

        post.error -> alert "error occured"
    ,
    createLanes: ->
        $(".connectedSortable").sortable({
            connectWith: ".connectedSortable",
            placeholder: "laneCardDrop",
            dropOnEmpty: true
        }).disableSelection()
    ,
    findLaneById: (id) ->
        l = for lane in this.lanes()
            lane if lane.Id == id
        return lane


ko.bindingHandlers.onReceiveItem = {
    init: (element, valueAccessor, allBindingsAccessor, viewModel) ->
        callback = valueAccessor()
        $(element).bind "sortreceive", (event, ui) ->
            receivedIds = $.map ui.item, (item) ->
                $(item).attr "data-id"

            callback.call(viewModel, receivedIds, ui.sender.context.id, event.target.id, ui.item.index())
}

$ ->
    $("#addCardLink").click ->
        $("#cardForm").toggle()
        return

    $("#addLaneLink").click ->
        $("#laneForm").toggle()
        return

    ko.applyBindings(viewModel)