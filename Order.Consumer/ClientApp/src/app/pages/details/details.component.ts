import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { InvoiceService, Invoice } from '../../services/invoice.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  product: Invoice;
  constructor(private route: ActivatedRoute, private invoiceService: InvoiceService, private snackBar: MatSnackBar, private router: Router) { }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(params => {
        this.invoiceService.getById(params.id).toPromise().then(res => {
          this.product = res.result;
        });
      });
  }
  public updateStatus(id: string, status: number): void {
    this.invoiceService.updateStatus(id, status).then(() => {
      this.openSnackBar(`Product ${this.getStatus(status)}`, 'Close');
      this.router.navigate(['product']);
    });
  }

  private openSnackBar(message: string, action: string): void {
    this.snackBar.open(message, action, {
      duration: 5000,
      politeness: "polite"
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
}
