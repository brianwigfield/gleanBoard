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
    newCardTitle: ko.observable("New Card"),
    newLaneName: ko.observable("New Lane"),
    addCard: ->
        $.post "/card/create",
               { board: viewModel.board, title: viewModel.newCardTitle, lane: $("#newCardLane").val(), position:1 },
               (data) -> 
                    viewModel.findLaneById(data.Lane).Cards.push(new card(data.Id, data.Title))
    ,
    addLane: ->
        $.post "/lane/create",
               { name: viewModel.newLaneName, board: viewModel.board, position: 0 },
               (data) -> viewModel.lanes.splice(0, 0, new lane(data.Id, data.Name, []))
    ,
    moveCard: (ids, from, to, position) ->
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
        (lane for lane in this.lanes() when lane.Id == id)[0]


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