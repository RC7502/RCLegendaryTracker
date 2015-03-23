$(function() {

    $("#getRandom").on("click", function () {
        
        var data = $("#randomOptionsForm").serialize();
        $.post("GetOptions", data, function(returndata) {
            var html = "Mastermind/Commander: " + returndata.Mastermind.FullName + "<br>";
            html += "</ul>Scheme/Plot: " + returndata.Scheme.FullName + "<br>";
            html += "Heroes/Allies:<ul>";
            for (var i = 0; i < returndata.Heroes.length; i++) {
                html += "<li>" + returndata.Heroes[i].FullName + "</li>";
            }
            html += "</ul>Villain/Adversary Groups:<ul>";
            for (i = 0; i < returndata.VillainGroups.length; i++) {
                html += "<li>" + returndata.VillainGroups[i].FullName + "</li>";
            }
            html += "</ul>Henchmen/Backup Groups:<ul>";
            for (i = 0; i < returndata.HenchmenGroups.length; i++) {
                html += "<li>" + returndata.HenchmenGroups[i].FullName + "</li>";
            }
            html += "</ul>";

            $("#resultDiv").html(html);
        });
    });

});