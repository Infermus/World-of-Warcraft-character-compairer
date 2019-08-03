function changeActiveState(event) {
    $(event.target).parent().find('.active').removeClass('active');
    $(event.target).addClass('active');
}

function changeRealmList(event, elementID) {
    changeActiveState(event);   

    $.ajax({
        method: 'get',
        url: 'CharacterComparator/GetRealmsList',
        data: { region: $(event.target).text() },
        dataType:'json',
        success: function (data) {
            var nodes = document.getElementById(elementID);
            var templateChild = nodes.lastElementChild;
            while (nodes.firstChild) {
                nodes.removeChild(nodes.firstChild);
            }

            for (let i = 0; i < data.length; ++i) {
                var div = document.createElement('a');
                div.setAttribute('href', '#');
                div.setAttribute('onclick', 'changeActiveState(event)');
                div.className = templateChild.className;
                div.innerHTML = data[i];
                nodes.appendChild(div);
            }
        }
    });
}