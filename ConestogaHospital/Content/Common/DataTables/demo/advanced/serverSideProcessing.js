'use strict';
angular.module('showcase.serverSideProcessing', ['datatables'])
.controller('ServerSideProcessingCtrl', ServerSideProcessingCtrl);

function ServerSideProcessingCtrl(DTOptionsBuilder, DTColumnBuilder) {
    //var vm = this;
    //vm.edit = edit;
    //vm.delete = deleteRow;
    //vm.dtInstance = {};
    //vm.cities = {};
    ////vm.dtOptions = DTOptionsBuilder.fromSource('data.json')
    ////    .withPaginationType('full_numbers');
    //vm.dtOptions = DTOptionsBuilder.newOptions()
    //     .withOption('ajax', {
    //         // Either you specify the AjaxDataProp here
    //         // dataSrc: 'data',
    //         url: '../../../../City/GetCityList', //'/angular-datatables/data/serverSideProcessing',
    //         type: 'POST'
    //     })
    //  // or here
    //  .withDataProp('data')
    //    .withOption('processing', true)
    //     .withOption('serverSide', true)
    //     .withPaginationType('full_numbers');
    //vm.dtColumns = [
    //    DTColumnBuilder.newColumn('CityId').withTitle('City Id'),
    //    DTColumnBuilder.newColumn('StateName').withTitle('State Name'),
    //    DTColumnBuilder.newColumn('CityName').withTitle('City Name'),//.notVisible()
    //DTColumnBuilder.newColumn(null).withTitle('Actions').notSortable()
    //        .renderWith(actionsHtml)
    //];
    
    //function edit(id) {
    //    vm.message = 'You are trying to edit the row with ID: ' + id;
    //    // Edit some data and call server to make changes...
    //    // Then reload the data so that DT is refreshed
    //    vm.dtOptions.reloadData();
    //};

    ////function edit(city) {
    ////    //function edit(city) {
    ////    vm.message = 'You are trying to edit the row: ' + JSON.stringify(city);
    ////    // Edit some data and call server to make changes...
    ////    // Then reload the data so that DT is refreshed
    ////    vm.dtInstance.reloadData();
    ////}
    //function deleteRow(city) {
    //    vm.message = 'You are trying to remove the row: ' + JSON.stringify(city);
    //    // Delete some data and call server to make changes...
    //    // Then reload the data so that DT is refreshed
    //    vm.dtInstance.reloadData();
    //}
    
    //function actionsHtml(data, type, full, meta) {
    //    vm.cities[data.CityId] = data;
    //    return '<button class="btn btn-warning" ng-click="edit(' + data.CityId + ')">' +
    //        '   <i class="fa fa-edit"></i>' +
    //        '</button>&nbsp;' +
    //        '<button class="btn btn-danger" ng-click="showCase.delete(showCase.cities[' + data.CityId + '])">' +
    //        '   <i class="fa fa-trash-o"></i>' +
    //        '</button>';
    //}
}

