import { HttpClient } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { FlexiGridModule, FlexiGridService, StateModel } from 'flexi-grid';
import { TrCurrencyPipe } from 'tr-currency';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FlexiGridModule, TrCurrencyPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
    users = signal<UserModel[]>([]);
    total = signal<number>(0);
    state = signal<StateModel>(new StateModel());
    loading = signal<boolean>(false);
    themeClass = signal<string>("light");

    constructor(
      private http: HttpClient,
      private flexi: FlexiGridService
    ){
      this.getAll();
    }

    changeThemeClass(){
      if(this.themeClass() === "light"){
        this.themeClass.set("dark")
      }else{
        this.themeClass.set("light")
      }
    }

    getAll(){
      this.loading.set(true);
      let enpoint ="https://localhost:7155/api/Users/GetAll?$count=true";
      let newEnpoint = this.flexi.getODataEndpoint(this.state());

      enpoint +=  "&" + newEnpoint;      

      this.http.get<{total: number, data: UserModel[]}>(enpoint).subscribe(res=> {
        this.users.set(res.data);
        this.total.set(res.total);
        this.loading.set(false);
      });
    }

    method(){
      console.log("excel export");
      
    }

    onDataStageChange(event:StateModel){
      this.state.set(event);
      this.getAll();
    }

}




export class UserModel{
  id: string = "";
  firstName: string = "";
  lastName: string = "";
  dateOfBirth: string = "";
  salary: number = 0;
}