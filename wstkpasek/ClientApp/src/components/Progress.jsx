import React, { Component } from "react";
import Chart from "./Chart";

export class Progress extends Component {
  static displayName = Progress.name;

  constructor(props) {
    super(props);
    this.state = { progress: [], loading: true };
  }

  async componentDidMount() {
    await this.fetchData();
  }

  fetchData = async () => {
    const progressResponse = await fetch("api/progress/part/Nogi", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const series = await progressResponse.json();
    this.setState({
      progressPart: series,
      loading: false,
    });
  };
  renderBody() {
    if (this.state.loading) {
      return <h2>Trwa ładowanie</h2>;
    } else {
      let dates = [];
      let loads = [];
      let times = [];
      let distances = [];
      let resttimes = [];
      let repeats = [];
      for (let i = 0; i < this.state.progressPart.length; i++) {
        dates.push(this.state.progressPart[i].date);
        loads.push(this.state.progressPart[i].loadAv);
        times.push(this.state.progressPart[i].timeAv);
        distances.push(this.state.progressPart[i].distanceAv);
        repeats.push(this.state.progressPart[i].repeatAv);
        resttimes.push(this.state.progressPart[i].restTimeAv);
      }
      let prepareDataSet = [];
      if (Math.max(...loads) > 0) {
        prepareDataSet.push({
          label: "Obciążenie",
          data: loads,
          borderColor: ["rgba(37,113,185,1)"],
          borderWidth: 1,
          backgroundColor: ["rgba(37,113,185,0.05)"],
        });
      }
      if (Math.max(...times) > 0) {
        prepareDataSet.push({
          label: "Czas",
          data: times,
          borderColor: ["rgba(37,113,185,1)"],
          borderWidth: 1,
          backgroundColor: ["rgba(37,113,185,0.2)"],
        });
      }
      if (Math.max(...distances) > 0) {
        prepareDataSet.push({
          label: "Dystans",
          data: distances,
          borderColor: ["rgba(37,113,185,1)"],
          borderWidth: 1,
          backgroundColor: ["rgba(37,113,185,0.05)"],
        });
      }
      if (Math.max(...repeats) > 0) {
        prepareDataSet.push({
          label: "Powtórzenia",
          data: repeats,
          borderColor: ["rgba(50,200,50,1)"],
          borderWidth: 1,
          backgroundColor: ["rgba(50,200,50,.05)"],
        });
      }
      if (Math.max(...resttimes) > 0) {
        prepareDataSet.push({
          label: "Odpoczynek",
          data: resttimes,
          borderColor: ["rgba(37,113,185,1)"],
          borderWidth: 1,
          backgroundColor: ["rgba(37,113,185,0.2)"],
        });
      }
      const chartData = {
        labels: dates,
        datasets: prepareDataSet,
      };
      let display = dates.length < 10 ? true : false;

      return (
        <React.Fragment>
          <div className="col-lg-8">
            <canvas id="progressChartByPart" width="600" height="300"></canvas>
            <Chart
              dataSet={chartData}
              title={
                "Średnie wartości w dniach dla " +
                this.state.progressPart[0].type
              }
              displayDates={display}
              chartId="progressChartByPart"
            />
          </div>
        </React.Fragment>
      );
    }
  }
  render() {
    return this.renderBody();
  }
}
