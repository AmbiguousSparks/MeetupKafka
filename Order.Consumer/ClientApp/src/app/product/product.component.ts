import { Component, OnInit } from "@angular/core";
import { Invoice, InvoiceService } from "app/services/invoice.service";

@Component({
  selector: "app-product",
  templateUrl: "./product.component.html",
  styleUrls: ["./product.component.css"],
})
export class ProductComponent implements OnInit {
  products: Invoice[];
  constructor(private invoiceService: InvoiceService) {
    this.invoiceService
      .getInvoices()
      .toPromise()
      .then((res) => {
        this.products = res;
      });
    this.invoiceService.invoiceUpdate.on("InvoiceUpdate", invoices => {
      this.products = invoices;
    });
  }

  public updateStatus(id: string, status: number): void {
    this.invoiceService.updateStatus(id, status);
  }
  ngOnInit(): void { }
}
