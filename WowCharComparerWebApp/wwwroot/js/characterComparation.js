function changeActiveState(event) {
    $(event.target).parent().find('.active').removeClass('active');
    $(event.target).addClass('active');
}

function changeRealmList(event, elementClass) {
    changeActiveState(event);

    $.ajax({
        method: 'get',
        url: 'CharacterComparator/GetRealmsList',
        data: { region: $(event.target).text() },
        dataType: 'json',
        success: function (data) {
            var nodes = document.getElementsByClassName(elementClass)[0];
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

function compareCharacter() {

    var characterFields = {
        ItemsField: $('.characters-fields-checkbox-group').children('.items-field').children().is(':checked'),
        StatisticsField: $('.characters-fields-checkbox-group').children('.statistics-field').children().is(':checked'),
        AchievementsField: $('.characters-fields-checkbox-group').children('.achievements-field').children().is(':checked'),
        ProgressionField: $('.characters-fields-checkbox-group').children('.progression-field').children().is(':checked'),
        PVPField: $('.characters-fields-checkbox-group').children('.pvp-field').children().is(':checked'),
        ReputationField: $('.characters-fields-checkbox-group').children('.reputation-field').children().is(':checked'),
        TalentsField: $('.characters-fields-checkbox-group').children('.talents-field').children().is(':checked'),
    };

    var firstCharacterData = {
        Name: $('.1st-character-nick.form-control').val(),
        ServerName: $('.1st-player-realms-list.list-group').children('.active')[0].innerText,
        Region: $('.1st-player-realm-group.btn-group.btn-group-toggle').children().children('.active')[0].innerText,
        ItemsField: characterFields.ItemsField,
        StatisticsField: characterFields.StatisticsField,
        AchievementsField: characterFields.AchievementsField,
        ProgressionField: characterFields.ProgressionField,
        PVPField: characterFields.PVPField,
        ReputationField: characterFields.ReputationField,
        TalentsField: characterFields.TalentsField
    };

    var secondCharacterData = {
        Name: $('.2nd-character-nick.form-control').val(),
        ServerName: $('.2nd-player-realms-list.list-group').children('.active')[0].innerText,
        Region: $('.2nd-player-realm-group.btn-group.btn-group-toggle').children().children('.active')[0].innerText,
        ItemsField: characterFields.ItemsField,
        StatisticsField: characterFields.StatisticsField,
        AchievementsField: characterFields.AchievementsField,
        ProgressionField: characterFields.ProgressionField,
        PVPField: characterFields.PVPField,
        ReputationField: characterFields.ReputationField,
        TalentsField: characterFields.TalentsField
    };

    $.ajax({
        method: 'post',
        url: 'CharacterComparator/CompareCharacters',
        data: {
            firstCharacter: firstCharacterData,
            secondCharacter: secondCharacterData
        },
    });
}

