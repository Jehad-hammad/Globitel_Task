import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Label } from 'ng2-charts';
import { NgxSpinnerService } from 'ngx-spinner';
import { Status, UserRoleArr, UserStatus } from '../../shared/enums/enums';
import { chartDto, UserProfileDTO } from '../../shared/models/models';
import { AuthService } from '../../shared/services/auth.service';
import { ExcelService } from '../../shared/services/excel.service';
import { NotificationService } from '../../shared/services/notification.service';
import { UsersService } from '../../shared/services/users.service';

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.scss']
})
/** UserList component*/
export class UserListComponent implements OnInit {
  /** UserList ctor */
 

  displayedColumns: string[] = ['fullName', 'userName', 'emailAddreess', 'userStatus', 'Actions'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  public dataSource: MatTableDataSource<any>

  public userRoles
  public userStatus

  public femaleAgeRange: number = 0;
  public maleAgeRange: number = 0;

  public chartData: chartDto = new chartDto();
  public usersList


  barChartOptions: ChartOptions = {
    responsive: true,
  };
  barChartLabels: Label[] = ['Male', 'Female'];
  barChartType: ChartType = 'bar';
  barChartLegend = true;
  barChartPlugins = [];

  barChartData: ChartDataSets[] = []
   


  searhObjectForm = new FormGroup({
    roleType: new FormControl(1),
    status : new FormControl(1)
  })

  constructor(
    private userService: UsersService,
    private excelService: ExcelService,
    private spinner: NgxSpinnerService,
    private notification: NotificationService,
    private authService:AuthService
  ) {

  }
  ngOnInit() {
    this.userRoles = UserRoleArr
    this.userStatus = UserStatus
    this.getChartData()
    this.getUserList()
  }

  getChartData() {
    this.userService.getChartData().subscribe(res => {
      this.chartData = res as chartDto;
      this.maleAgeRange = this.chartData.maleAgeRange
      this.femaleAgeRange = this.chartData.femaleAgeRange
      this.barChartData = [{
        backgroundColor: ['rgba(54, 162, 235, 0.2)', 'rgba(255, 99, 132, 0.2)'],
        borderColor: [
          'rgb(54, 162, 235)',
          'rgb(255, 99, 132)',
        ],
        borderWidth: 1,
        data: [this.maleAgeRange, this.femaleAgeRange, 50, 100], label: 'Age Range'
      }
  ];
    })
  }

  chartConfig() {

  }
  getUserList() {
    const searchObject = this.searhObjectForm.getRawValue();
    this.userService.getListOfUsers(searchObject).subscribe(res => {
      this.usersList = res as any[]
      this.dataSource = new MatTableDataSource(this.usersList)
      this.dataSource.paginator = this.paginator;
    })
  }

  getUserStatus(id) {
    return this.userStatus.find(x=>x.id == id).name
  }
  onSearch() {
    this.getUserList()
  }

  onGenerateExcelReport() {
    this.spinner.show()
    this.excelService.generateActiveEmployeesExcel().subscribe(res => {
      this.downLoadFile(res, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
      this.spinner.hide()
    }, error => {
      this.spinner.hide()
      this.notification.showNotification('Report', 'Something went wrong while processing your request' ,'error')
    })
  }
  downLoadFile(data: any, type: string) {
    var blob = new Blob([data], { type: type.toString() });
    var url = window.URL.createObjectURL(blob);
    var pwa = window.open(url);
    if (!pwa || pwa.closed || typeof pwa.closed == 'undefined') {
      alert('Please disable your Pop-up blocker and try again.');
    }
  }
  OnlogOut() {
    this.authService.logout();
  }

  OnDisableUser(element)
  {
    debugger
    this.spinner.show()
    const disabledUser: UserProfileDTO = element
    disabledUser.status = Status.Disable
    this.userService.updateUserInfo(disabledUser).subscribe(res => {
      this.spinner.hide()
      this.getUserList()
      this.getChartData()
      this.notification.showNotification('User', 'User disabled successfully', 'success')
    }, error => {
      this.spinner.hide()
      this.notification.showNotification('User','Something went wrong while processing your request','error')
    })
  }
  OnActivateUser(element)
  {
    this.spinner.show()
    const activateUser: UserProfileDTO = element
    activateUser.status = Status.Active
    this.userService.updateUserInfo(activateUser).subscribe(res => {
      this.spinner.hide()
      this.notification.showNotification('User', 'User Activated Successfully', 'success')
      this.getUserList()
      this.getChartData()
    }, error => {
      this.spinner.hide()
      this.notification.showNotification('User', 'Something went wrong while processing your request','error')
    })
  }
}
