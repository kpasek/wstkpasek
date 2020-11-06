import React, { Component } from "react";
import Chart from "./Chart";
import SelectParts from "./SelectParts";

export class Progress extends Component {
  static displayName = Progress.name;

  constructor(props) {
    super(props);
    this.state = { progressPart: [], loadChart: true, loading: true };
  }

  async componentDidMount() {
    await this.fetchData();
  }

  fetchData = async () => {
    const partResponse = await fetch("api/exercises/parts", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const parts = await partResponse.json();
    const progressResponse = await fetch("api/progress/part/" + parts[0].name, {
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
      parts: parts,
      loading: false,
    });
  };

  handleChangePart = async () => {
    const part = document.getElementById("select-parts").value;
    this.setState({
      loadChart: false,
    });
    const progressResponse = await fetch("api/progress/part/" + part, {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const progress = await progressResponse.json();
    this.setState({
      progressPart: progress,
      loadChart: true,
    });
  };
  prepareDates() {
    let dates = [];
    for (let i = 0; i < this.state.progressPart.length; i++) {
      dates.push(this.state.progressPart[i].date);
    }
    return dates;
  }
  prepareDataSet() {
    let loads = [];
    let times = [];
    let distances = [];
    let resttimes = [];
    let repeats = [];
    for (let i = 0; i < this.state.progressPart.length; i++) {
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
    return prepareDataSet;
  }
  renderChartComponent(chartData, display) {
    if (this.state.loadChart) {
      return (
        <Chart
          dataSet={chartData}
          title={
            this.state.progressPart[0] !== undefined
              ? "Średnie wartości w dniach dla " +
                this.state.progressPart[0].type
              : "Brak danych w wybranym okresie"
          }
          displayDates={display}
          chartId={"progressChartByPart"}
        />
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
      const display = chartData.labels.length < 10 ? true : false;

      return (
        <React.Fragment>
          <SelectParts onChangePart={this.handleChangePart} default={false} />
          <div className="col-lg-8">
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
