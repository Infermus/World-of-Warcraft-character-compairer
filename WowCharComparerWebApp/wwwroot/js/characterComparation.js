function changeRealmList(event) {
    $('.region-selection-button').removeClass('active');
    $(event.target).addClass('active');

    $.ajax({
        method: 'get',
        url: 'CharacterComparator/GetRealmsList',
        data: { region: $(event.target).text() },
        dataType:'json',
        success: function (data) {
            var nodes = document.getElementById("1st-player-realms-list list-group");
            //while (nodes.firstChild) {
            //    nodes.removeChild(nodes.firstChild);
            //}

            var node = document.createElement("list-group-item list-group-item-action");
            var textnode = document.createTextNode("Water");
            node.appendChild(textnode); 
            console.log(node);
            document.getElementById("1st-player-realms-list list-group").appendChild(node);

            //for (let i = 0; i < data.length; ++i) {
            //}

            //$("1st-player-realms-list list-group").addClass("list-group-item list-group-item-action", data[0]);
        }
    });
}