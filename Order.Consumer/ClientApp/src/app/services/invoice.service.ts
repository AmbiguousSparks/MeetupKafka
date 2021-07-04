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
  
  getInvoices(): Observable<Invoice[]> {
    return this.httpClient.get<Invoice[]>("Invoice/GetAll");
  }
  getById(id: string): Observable<Invoice> {
    return this.httpClient.get<Invoice>("Invoice/GetById?id=" + id);
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
      this.getInvoices().toPromise().then(invoices => {
        console.log(invoices);
      });
    });
  }
}

export interface Invoice {
  name: string;
  id: string;
  photo: string;
  value: number;
  category: number;
  features: string[];
}
