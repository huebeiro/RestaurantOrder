import { Component, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
})
export class MainComponent {

  private http: HttpClient;
  private apiUrl: string;
  private localStorageKey: string = 'ROA_STORAGE';

  public currentInput: string;
  public currentOrder: Order;
  public pastOrders: PastOrder[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    this.http = http;
    this.apiUrl = apiUrl;

    this.currentOrder = {
      output: null,
      error: null
    }
    this.pastOrders = this.retreiveLocalStorageOrders();

    this.currentInput = "morning, 1, 2, 3"; //TODO remove
  }

  public makeOrder() {
    this.http.get<Order>(this.apiUrl + 'order/' + this.currentInput)
      .subscribe(
        result => this.handleResult(result),
        error => this.handleError(error)
      );
  }

  private handleResult(result: Order) {
    this.currentOrder = result;
    if (this.currentOrder.output) {
      this.pastOrders.push(
        {
          input: this.currentInput,
          output: this.currentOrder.output
        }
      );
      this.updateLocalStorageOrders();
    }
  }

  private handleError(error: HttpErrorResponse) {
    console.error(error);
    this.currentInput = null;
    this.displayError(error.error.input);
  }

  private displayError(error: string) {
    if (error) {
      alert(error);
      return;
    }
    alert("There is something wrong with your order.");
  }

  private updateLocalStorageOrders() {
    localStorage.setItem(
      this.localStorageKey,
      JSON.stringify(this.pastOrders)
    );
  }

  private retreiveLocalStorageOrders(): PastOrder[] {
    var jsonOrders = localStorage.getItem(this.localStorageKey);

    if (!jsonOrders) {
      return [];
    }

    return JSON.parse(jsonOrders);
  }

  public clearHistory() {
    this.pastOrders = [];
    this.updateLocalStorageOrders();
  }
}

interface Order {
  output: string;
  error: string;
}

interface PastOrder {
  input: string;
  output: string;
}
