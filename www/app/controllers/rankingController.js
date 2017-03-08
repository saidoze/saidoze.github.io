leagueRankingApp.controller('RankingController', function ($scope, $ionicSideMenuDelegate, $location, myConfig, $firebaseObject, $ionicLoading) {
    var ref = firebase.database().ref();
    var fb = $firebaseObject(ref);


    $scope.navTitle = 'Home Page';
    $scope.rankings = [
      {name: 'Tobias', matchesPlayed: 3, goalsScored: 27, points: 7},
      {name: 'Pj', matchesPlayed: 3, goalsScored: 21, points: 4},
      {name: 'Quicky', matchesPlayed: 3, goalsScored: 14, points: 2},
    ];

    $scope.test = function () {
      // $ionicLoading.show({
      //   template: '<ion-spinner class="spinner-calm"></ion-spinner>'
      // }).then(function(){
      //   console.log("The loading indicator is now displayed");
      // });
      
      fb.players = [
        {name: 'Tobias'},
        {name: 'Pj'},
        {name: 'Quicky'},
      ];
      
      console.log("players",players);
      fb.$save().then(function(ref) {
        ref.key === fb.$id; // true
        console.log("ref", ref);
        console.log("players.$id", fb.$id);
      }, function(error) {
        console.log("Error:", error);
      });
    };

    $scope.load = function() {

      fb.$loaded()
        .then(function(data) {
          console.log("loaded record:", fb.$id, fb.someOtherKeyInData);
          // To iterate the key/value pairs of the object, use angular.forEach()
          angular.forEach(fb, function(value, key) {
              console.log(key, value);
          });
        })
        .catch(function(error) {
          console.error("Error:", error);
        });
    }
});