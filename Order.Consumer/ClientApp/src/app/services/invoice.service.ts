import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private _hubConnection: HubConnection;
  constructor(private httpClient: HttpClient) {
    this.createConnection();
    this.startConnection();
    this.registerServerEvents();
  }
  
  newInvoice(): Observable<Invoice[]> {
    return this.httpClient.get<Invoice[]>("Home/Index");
  }
  private connect(name: string): void {
    this._hubConnection.invoke("Connect", name);
  }
  private createConnection(): void {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl("/invoice-hub")
      .build();
  }
  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        console.log("Connected");
        this.connect("Daniel");
      }).catch(error => {
        console.log(error);
      });
  }
  private registerServerEvents(): void {
    this._hubConnection.on("NewInvoice", () => {
      this.newInvoice().toPromise().then(invoices => {
        console.log(invoices);
      });
    });
  }
}

export interface Invoice {
  name: string;
  _id: string;
}
