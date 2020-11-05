import React, { Component } from "react";
import Chart from "./Chart";

export class Progress extends Component {
  static displayName = Progress.name;

  constructor(props) {
    super(props);
    this.state = { loading: true };
  }

  async componentDidMount() {
    await this.fetchData();
  }

  fetchData = async () => {
    const progressResponse = await fetch("api/progress", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const series = await progressResponse.json();
    console.log(series);
    this.setState({
      progress: series,
      loading: false,
    });
  };
  renderBody() {
    if (this.state.loading) {
      return <h2>Trwa ładowanie</h2>;
    } else {
      const dataSet = {
        dataType: this.state.progress[120].part,
        labels: this.state.progress[120].dates,
        data: this.state.progress[120].loads,
      };
      return (
        <React.Fragment>
          <div className="col-lg-8">
            <canvas id="myChart" width="600" height="300"></canvas>
            <Chart dataSet={dataSet} />
          </div>
        </React.Fragment>
      );
    }
  }
  render() {
    return this.renderBody();
  }
}
