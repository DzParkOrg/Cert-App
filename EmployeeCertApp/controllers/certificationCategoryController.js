app.controller('certificationCategoryController', ['$scope', '$location', function ($scope, $location) {

    $scope.treeOptions = {
        nodeChildren: "children"
        , dirSelectable: true
        , injectClasses: {
            ul: "a1"
            , li: "a2"
            , liSelected: "a7"
            , iExpanded: "a3"
            , iCollapsed: "a4"
            , iLeaf: "a5"
            , label: "a6"
            , labelSelected: "a8"
        }
    };

    $scope.currentNode = '';
    $scope.newCategory = '';


    /*$scope.addRoot = function() {
        $scope.dataForTheTree.push({name: $scope.newCategory, children: []})
    };*/


    $scope.addNode = function (node) {
        //console.log('node '+node);
        if ($scope.newCategory == '') {
            $scope.message = "empty node can't be added";
        } else if (node === "" && $scope.newCategory !== null) {
            $scope.dataForTheTree.push({
                name: $scope.newCategory
                , children: []
            });
            $scope.newCategory = null;
            $scope.successmessage = "technology added successfully.";
        } else {
            node.children.push({
                name: $scope.newCategory
                , children: []
            })
        }

        // $scope.dataForTheTree.children[num].push({name: $scope.newCategory, children : []})
        $scope.currentNode = '';
    };

    $scope.deleteNode = function (node) {
        $scope.dataForTheTree.pop({
            name: $scope.newCategory
            , children: []
        });
    }

    $scope.selectNode = function (num) {
        $scope.selected = $scope.dataForTheTree[num];
    };


    $scope.showSelected = function (node) {
        console.log(node);
        $scope.currentNode = node;

    };

    $scope.dataForTheTree = [
        /*
            { "name" : "Java", "children" : [
                { "name" : "Core-Java", "children" : [] },
                { "name" : "Advance-java", "children" : [
                    
                    { "name" : "Oracle",  "children" : [
                        { "name" : "OCJP",  "children" : [] },
                        { "name" : "OCJP2",  "children" : [] }
                    ]}
                ]}
            ]},
            { "name" : "Android",  "children" : [] },
            { "name" : "Sql",  "children" : [
               
                 { "name" : "MySql",  "children" : [
                        { "name" : "SQlite", "children" : [] },
                        { "name" : "PlSql",  "children" : [] }
                    ]}
              
            ]}*/
];


}]);