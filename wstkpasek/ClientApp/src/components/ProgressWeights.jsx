import React, { Component } from "react";
import Chart from "./Chart";

export default class ProgressWeights extends Component {
  constructor(props) {
    super(props);
    this.state = {
      ProgressWeights: [],
      chartId: "progressWeights",
      dateFromId: "dateFromWeightId",
      dateToId: "dateToWeightId",
      loadChart: true,
      loading: true,
    };
  }

  async componentDidMount() {
    await this.fetchData();
  }
  fetchData = async () => {
    const weightsResponse = await fetch("api/weights", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const weights = await weightsResponse.json();
    this.setState({
      ProgressWeights: weights,
      loading: false,
    });
  };
  handleDate = async () => {
    this.setState({
      loadChart: false,
    });
    const dateFrom = document.getElementById(this.state.dateFromId).value;
    const dateTo = document.getElementById(this.state.dateToId).value;
    const weightsResponse = await fetch(
      "api/weights/dates?dateFrom=" + dateFrom + "&dateTo=" + dateTo,
      {
        method: "GET",
        mode: "cors",
        cache: "no-cache",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    const weights = await weightsResponse.json();
    this.setState({
      ProgressWeights: weights,
      loadChart: true,
    });
    document.getElementById(this.state.chartId).scrollIntoView();
  };

  prepareDates() {
    let dates = [];
    for (let i = 0; i < this.state.ProgressWeights.length; i++) {
      dates.push(this.state.ProgressWeights[i].date.substr(5, 5));
    }
    return dates;
  }
  prepareDataSet() {
    let weights = [];

    for (let i = 0; i < this.state.ProgressWeights.length; i++) {
      weights.push(this.state.ProgressWeights[i].weightKg);
    }
    let prepareDataSet = [];
    if (Math.max(...weights) > 0) {
      prepareDataSet.push({
        label: "Waga",
        data: weights,
        borderColor: ["rgba(37,113,185,1)"],
        borderWidth: 1,
        backgroundColor: ["rgba(37,113,185,0.05)"],
      });
    }

    return prepareDataSet;
  }
  renderChartComponent(chartData, display) {
    if (this.state.loadChart) {
      return (
        <Chart
          dataSet={chartData}
          title={
            this.state.ProgressWeights[0] !== undefined
              ? "Waga"
              : "Brak danych w wybranym okresie"
          }
          displayDates={display}
          chartId={this.state.chartId}
        />
      );
    } else {
      return (
        <div className="chart-reload">
          <h2 className="text-center">Trwa ładowanie wykresu</h2>
        </div>
      );
    }
  }
  renderBody() {
    if (this.state.loading) {
      return <h2>Trwa ładowanie</h2>;
    } else {
      const chartData = {
        labels: this.prepareDates(),
        datasets: this.prepareDataSet(),
      };
      const display = chartData.labels.length < 15 ? true : false;

      return (
        <React.Fragment>
          <div className="form-row mx-auto my-3">
            <div className="col-12">
              <h3>Waga</h3>
            </div>
            <div className="col">
              <label htmlFor={this.state.dateFromId}>Okres od: </label>
              <input
                type="date"
                className="form-control"
                id={this.state.dateFromId}
                onChange={this.handleDate}
                defaultValue={new Date(
                  new Date().setMonth(new Date().getMonth() - 3)
                )
                  .toISOString()
                  .slice(0, 10)}
              />
            </div>
            <div className="col">
              <label htmlFor={this.state.dateToId}> do: </label>
              <input
                type="date"
                className="form-control"
                id={this.state.dateToId}
                onChange={this.handleDate}
                defaultValue={new Date().toISOString().slice(0, 10)}
              />
            </div>
          </div>
          <div className="mx-auto">
            {this.renderChartComponent(chartData, display)}
          </div>
        </React.Fragment>
      );
    }
  }
  render() {
    return this.renderBody();
  }
}
