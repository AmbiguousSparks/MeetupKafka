import { HttpClient } from '@angular/common/http';
import { Injectable, Output } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { EventEmitter } from 'events';
import { Observable } from 'rxjs';
import { Response } from '../models/response';

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

  private _host = window.location.origin + "/";

  getInvoices(): Observable<Response<Invoice[]>> {
    return this.httpClient.get<Response<Invoice[]>>(this._host + "api/Product/GetAllPending");
  }
  getById(id: string): Observable<Response<Invoice>> {
    return this.httpClient.get<Response<Invoice>>(this._host + "api/Product/Get?id=" + id);
  }
  private connect(name: string): void {
    this._hubConnection.invoke("Connect", name);
  }
  updateStatus(id: string, status: number): Promise<void> {
    return new Promise((s, f) => {
      let body = {
        id,
        status
      };
      this.httpClient.post<Response<Invoice>>(this._host + "api/Product/UpdateStatus", body).toPromise().then(() => {
        this.invoiceUpdateHandler();
        s();
      }).catch(err => {
        console.error(err);
        f();
      });
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
      this.invoiceUpdate.emit("InvoiceUpdate", invoices.result);
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
  features: {};
}
