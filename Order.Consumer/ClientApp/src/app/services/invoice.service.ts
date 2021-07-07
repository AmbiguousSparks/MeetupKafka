import { HttpClient } from '@angular/common/http';
import { Injectable, Output } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { EventEmitter } from 'events';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private _hubConnection: HubConnection;
  @Output() invoiceUpdate = new EventEmitter();
  constructor(private httpClient: HttpClient) {
    this.createConnection();
    this.startConnection();
    this.registerServerEvents();
  }

  private _host = window.location.origin + window.location.pathname;
  
  getInvoices(): Observable<Invoice[]> {
    return this.httpClient.get<Invoice[]>(this._host + "/api/Product/GetAllPending");
  }
  getById(id: string): Observable<Invoice> {
    return this.httpClient.get<Invoice>(this._host + "/api/Product/GetById?id=" + id);
  }
  private connect(name: string): void {
    this._hubConnection.invoke("Connect", name);
  }
  updateStatus(id: string, status: number): void {
    let body = {
      id,
      status
    };
    this.httpClient.post<Invoice>(this._host + "/api/Product/UpdateStatus", body).toPromise().then(()=> {
      this.invoiceUpdateHandler();
    }).catch(err => {
      console.error(err);
    });
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
  private invoiceUpdateHandler(): void {
    this.getInvoices().toPromise().then(invoices => {
      this.invoiceUpdate.emit("InvoiceUpdate", invoices);
    });
  }
  private registerServerEvents(): void {
    this._hubConnection.on("NewInvoice", () => {
      this.invoiceUpdateHandler();
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
