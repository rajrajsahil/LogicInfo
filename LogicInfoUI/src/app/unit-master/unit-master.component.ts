import { Component, OnInit, ViewChild } from '@angular/core';
import { IgxGridComponent } from 'igniteui-angular';
import { MainServiceService } from '../service/main-service.service';
import { UnitMaster } from './unit-master';

@Component({
  selector: 'app-unit-master',
  templateUrl: './unit-master.component.html',
  styleUrls: ['./unit-master.component.scss']
})
export class UnitMasterComponent implements OnInit {

  @ViewChild('unitMasterRef')
  public unitMasterRef: IgxGridComponent | undefined;
  //@ts-ignore
  unitName: string;
  //@ts-ignore
  description: string;
  //@ts-ignore
  group: string;
  unitMasterGridData: any[] = [];
  constructor(
    private readonly mainService: MainServiceService
  ) {
    this.getGridData();
   }

  ngOnInit(): void {
  }
  getGridData() {
    this.mainService.getAllUnitMaster().subscribe(data => {
      this.unitMasterGridData = data;
      console.log(data);
    });
  }
  add() {
    const unitMaster = new UnitMaster();
    unitMaster.name = this.unitName;
    unitMaster.group = this.group;
    unitMaster.desc = this.description;
    if (unitMaster.name === undefined || unitMaster.group === undefined || unitMaster.desc === undefined) {
      console.log('ALl Column is not filled');
    }
    this.mainService.addUnitMaster(unitMaster).subscribe(data => {
      console.log(data);
      this.getGridData();
    });

  }
  modify() {

  }
  delete() {
    console.log(this.unitMasterRef);
    const deletingRow = this.unitMasterRef?.selectedRows;
    if (deletingRow === undefined) {
      console.log('Row Not selected');
    }
     else {
      this.mainService.deleteFromUnitMaster(deletingRow[0] as UnitMaster).subscribe(data => {
        this.getGridData();
      });
     }
  }
  exit() {
    
  }

}
