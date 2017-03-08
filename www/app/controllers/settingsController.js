leagueRankingApp.controller('SettingsController', function ($scope, $ionicSideMenuDelegate, $location, myConfig, $firebaseObject, $firebaseArray, $ionicLoading, $ionicPopup, $ionicTabsDelegate, ionicDatePicker) {
    var ref = firebase.database().ref('players'); //var ref = new Firebase('https://sport-league-creator.firebaseio.com/players/');
    var playerList = $firebaseArray(ref);
    var gamedatesList = $firebaseArray(firebase.database().ref('gamedates'));

    //var fb = $firebaseObject(ref); // this loads all the data!
    $ionicLoading.show({template: '<ion-spinner class="spinner-calm"></ion-spinner>'});

    //$scope.data = fb;
    $scope.players = [];
    $scope.gamedates = [];
    $scope.newPlayer = {};
    $scope.addButtonVisible = true;

    playerList.$loaded()
        .then(function(data) {
          console.log("loaded playerList:", data);
          $ionicLoading.hide();
          $scope.players = data;
        })
        .catch(function(error) {
          console.error("Error:", error);
        });
    gamedatesList.$loaded()
        .then(function(data) {
          console.log("loaded gamedatesList:", data);
          $scope.gamedates = data;
        })
        .catch(function(error) {
          console.error("Error:", error);
        });
    
    function init() {
        //$ionicLoading.hide();
    }

    $scope.switchTabs = function(index) {
        $ionicTabsDelegate.select(index);
        switch(index) {
            case 0:
            case 1:
                $scope.addButtonVisible = true;
                break;
            case 2:
                $scope.addButtonVisible = false;
                break;
        }
    }

    $scope.add = function() {
        if($ionicTabsDelegate.selectedIndex() == 0)
            addPlayer();
        else if($ionicTabsDelegate.selectedIndex() == 1)
            addGamedate();
    }

    function addGamedate(){
        var ipObj1 = {
            callback: function (val) {  //Mandatory
                console.log('Return value from the datepicker popup is : ' + val, new Date(val));
                gamedatesList.$add({ dateval: val });
            }, 
            disabledDates: [            //Optional
                new Date(2017, 2, 6),
            ],
            inputDate: new Date(2017, 2, 9),
        }
        ionicDatePicker.openDatePicker(ipObj1);
    }

    function addPlayer() {
        $scope.newPlayer = {};
        var newPlayerPopup = $ionicPopup.show({
            template: '<input type="text" ng-model="newPlayer.name">',
            title: 'Add',
            subTitle: '',
            scope: $scope,
            buttons: [
                { text: 'Cancel' },
                {
                    text: '<b>Save</b>',
                    type: 'button-positive',
                    onTap: function(e) {
                        if (!$scope.newPlayer.name) {
                            //don't allow the user to close unless he enters wifi password
                            e.preventDefault();
                        } else {
                            return $scope.newPlayer.name;
                        }
                    }
                }
            ]
        });

        newPlayerPopup.then(function(res) {
            //save new player name
            playerList.$add({ name: res });
        });
    }

    $scope.remove = function(item) {
        if($ionicTabsDelegate.selectedIndex() == 0)
            playerList.$remove(item);
        else if($ionicTabsDelegate.selectedIndex() == 1)
            gamedatesList.$remove(item);
    }
    



    
    // $scope.rankings = [
    //   {name: 'Tobias', matchesPlayed: 3, goalsScored: 27, points: 7},
    //   {name: 'Pj', matchesPlayed: 3, goalsScored: 21, points: 4},
    //   {name: 'Quicky', matchesPlayed: 3, goalsScored: 14, points: 2},
    // ];

    // $scope.test = function () {
    //   // $ionicLoading.show({
    //   //   template: '<ion-spinner class="spinner-calm"></ion-spinner>'
    //   // }).then(function(){
    //   //   console.log("The loading indicator is now displayed");
    //   // });
      
    //   fb.players = [
    //     {name: 'Tobias'},
    //     {name: 'Pj'},
    //     {name: 'Quicky'},
    //   ];
      
    //   console.log("players",players);
    //   fb.$save().then(function(ref) {
    //     ref.key === fb.$id; // true
    //     console.log("ref", ref);
    //     console.log("players.$id", fb.$id);
    //   }, function(error) {
    //     console.log("Error:", error);
    //   });
    // };

    // $scope.load = function() {

    //   fb.$loaded()
    //     .then(function(data) {
    //       console.log("loaded record:", fb.$id, fb.someOtherKeyInData);
    //       // To iterate the key/value pairs of the object, use angular.forEach()
    //       angular.forEach(fb, function(value, key) {
    //           console.log(key, value);
    //       });
    //     })
    //     .catch(function(error) {
    //       console.error("Error:", error);
    //     });
    // }
});