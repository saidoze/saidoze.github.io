leagueRankingApp.controller('SettingsController', function ($q, $timeout, $scope, $state, $ionicSideMenuDelegate, $ionicHistory, $location, myConfig, $firebaseObject, $firebaseArray, $ionicLoading, $ionicPopup, $ionicListDelegate, $ionicTabsDelegate, ionicDatePicker) {
    $scope.players = $firebaseArray(firebase.database().ref('players')); 
    $scope.gamedates = $firebaseArray(firebase.database().ref('gamedates'));
    $scope.games = $firebaseArray(firebase.database().ref('games'));
    
    $ionicLoading.show({template: '<ion-spinner class="spinner-calm"></ion-spinner>'});

    $scope.newPlayer = {};
    $scope.addButtonVisible = true;

    $q.all([$scope.players, $scope.gamedates, $scope.games]).then(function(results) {
        console.log("ALL PROMISES RESOLVED");
        console.log(results);
        $timeout(function() {
            $ionicLoading.hide();
        }, 1000);
    }).catch(function(error) {
        console.error("Error:", error);
    });

    $scope.doWhenDone = function() {
        $ionicLoading.hide();
    };

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

    $scope.closeGamedate = function(item) {
        $ionicHistory.nextViewOptions({
            disableBack: true
        });
        $state.go('closeGamedate', { gamedateId: item.$id });
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
                $scope.gamedates.$add({ dateval: val });
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
            template: '<input type="text" ng-model="newPlayer.name" autofocus>',
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
            $scope.players.$add({ name: res });
        });
    }

    $scope.remove = function(item) {
        if($ionicTabsDelegate.selectedIndex() == 0)
            showConfirm('Remove player', 'Are you sure you want to remove this player?', removeGamedate, undefined, item);
        else if($ionicTabsDelegate.selectedIndex() == 1)
            showConfirm('Remove date', 'Are you sure you want to remove this date?', removeGamedate, undefined, item);
    }
    function removeGamedate(item) {
        gamedates.$remove(item);
        $ionicListDelegate.closeOptionButtons();
    }
    function removePlayer(item) {
        gamedates.$remove(item);
        $ionicListDelegate.closeOptionButtons();
    }
    
    function showConfirm(title, template, functionYes, functionNo, item) {
        var confirmPopup = $ionicPopup.confirm({
            title: title,
            template: template
        });

        confirmPopup.then(function(res) {
            if(res) {
                functionYes(item);
            } else {
                if(functionNo != undefined)
                    functionNo(item);
                else
                    $ionicListDelegate.closeOptionButtons();
            }
        });
    };

});