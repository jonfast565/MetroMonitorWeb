home.controller('metroMonitor-home-controller', ['$scope', '$http', '$interval', '$timeout', function ($scope, $http, $interval, $timeout) {
    $scope.home = new function () {
        var utilities = {
            loadRailPredictions: function () {
                var scheduleFunction = function() {
                    $http({ method: 'GET', url: 'api/RailPredictions/' })
                    .success(function (data) {
                        if (data != null) {
                            api.railPredictions = utilities.filterInvalidPredictions(data)
                        }
                    })
                }
                $timeout(scheduleFunction, 1000)
                $interval(scheduleFunction, 10000)
            },
            loadStations: function() {
                $http({ method: 'GET', url: 'api/RailStations' })
                .success(function (data) {
                    api.railStations = data
                })
            },
            loadLines: function() {
                $http({ method: 'GET', url: 'api/RailLines' })
                .success(function (data) {
                    api.railStations = data
                })
            },
            stripInvalidCarNumbers: function (railPredictions) {
                for (var i = 0; i < railPredictions.length; i++) {
                    if (railPredictions[i].CarNumber === "-" ||
                        railPredictions[i].CarNumber.replace(" ", "") === "") {
                        railPredictions[i].CarNumber = null
                    }
                }
                return railPredictions
            },
            filterInvalidPredictions: function (railPredictions) {
                return _.filter(utilities.stripInvalidCarNumbers(railPredictions),
                    function (prediction) {
                    return prediction.CarNumber != null
                })
            },
            initialize: function () {
                this.loadLines()
                this.loadStations()
                this.loadRailPredictions()
            }
        }

        var api = {
            railPredictions: [],
            railStations: [],
            railLines: [],
            currentSort: 'LineCode',
            sorts: [
                { name: "Line", field: "LineCode" },
                { name: "ETA", field: "MinutesToArrival"}
            ],
            getColorCode: function (code) {
                switch (code) {
                    case "YL":
                        return "yellow"
                    case "GR":
                        return "green"
                    case "BL":
                        return "blue"
                    case "SV":
                        return "lightgrey"
                    case "RD":
                        return "red"
                    case "OR":
                        return "orange"
                    default:
                        return "black"
                }
            },
            getRailPredictions: function() {
                return _.sortBy(api.railPredictions, api.currentSort)
            },
            getNullableValue: function (value) {
                if (!value || value === "") {
                    return "NA"
                } else return value
            },
            getStationNameFromCode: function (code) {
                var found = _.find(api.railStations, function (station) {
                    return station.StationCode === code
                })
                if (found) return found.Name
                return null
            },
            getStationInfo: function (locationStationCode, destinationStationCode) {
                var first = api.getStationNameFromCode(locationStationCode)
                var second = api.getStationNameFromCode(destinationStationCode)
                if (second != null) {
                    return first + " - " + second
                } else {
                    return first
                }
            }
        }

        utilities.initialize()
        return api
    }
}])