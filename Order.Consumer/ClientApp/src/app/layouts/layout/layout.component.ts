import { Component, OnInit } from '@angular/core';
import 'rxjs/add/operator/filter';
import { InvoiceService } from '../../services/invoice.service';
@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  constructor(private invoiceService: InvoiceService) { }

  ngOnInit() {
  }

}
