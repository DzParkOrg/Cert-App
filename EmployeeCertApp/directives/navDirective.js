app.directive('navMenu', function () {
    var directive = {};

    directive.restrict = 'E';

    directive.controller = 'navController';

    directive.templateUrl = 'views/navigationBar.html';

    directive.scope = {
        //accordionPreference: "=accordionPreference",
        //leadData: "=leadData"
    }

    return directive;
})