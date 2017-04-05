leagueRankingApp.controller('ResultsController', function ($q, $timeout, $scope, $state, $ionicSideMenuDelegate, $ionicHistory, $location, myConfig, $firebaseObject, $firebaseArray, $ionicLoading, $ionicPopup, $ionicListDelegate, $ionicTabsDelegate, ionicDatePicker) {
    $scope.players = $firebaseArray(firebase.database().ref('players')); 
    $scope.gamedates = $firebaseArray(firebase.database().ref('gamedates'));
    $scope.games = $firebaseArray(firebase.database().ref('games'));
    
    $ionicLoading.show({template: '<ion-spinner class="spinner-calm"></ion-spinner>'});

    

    $q.all([$scope.players, $scope.gamedates, $scope.games]).then(function(results) {
        console.log("ALL PROMISES RESOLVED");
        console.log(results);
        //$scope.doWhenDone();
    }).catch(function(error) {
        console.error("Error:", error);
    });

    $scope.doWhenDone = function() {
        $ionicLoading.hide();
    };
    

});