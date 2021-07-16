import { Component, OnInit } from "@angular/core";
import { Invoice, InvoiceService } from "app/services/invoice.service";
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: "app-product",
  templateUrl: "./product.component.html",
  styleUrls: ["./product.component.css"],
})
export class ProductComponent implements OnInit {
  products: Invoice[];
  constructor(private invoiceService: InvoiceService, private snackBar: MatSnackBar) {

  }

  public updateStatus(id: string, status: number): void {
    this.invoiceService.updateStatus(id, status).then(() => {
      this.openSnackBar(`Product ${this.getStatus(status)}`, 'Close');
    });
  }

  private getStatus(status: number): string {
    switch (status) {
      case 2:
        return "Approved";
      case 3:
        return "Repproved";
      default:
        "";
    }
  }
  ngOnInit(): void {
    this.invoiceService
      .getInvoices()
      .toPromise()
      .then((res) => {
        this.products = res.result;
      });
    this.invoiceService.invoiceUpdate.on("InvoiceUpdate", invoices => {
      this.products = invoices;
    });
  }

  private openSnackBar(message: string, action: string): void {
    this.snackBar.open(message, action, {
      duration: 5000,
      politeness:"polite" 
    });
  }

}
