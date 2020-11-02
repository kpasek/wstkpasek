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
      series: series,
    });
  };
  renderBody() {
    if (this.state.loading) {
      return <h2>Trwa ładowanie</h2>;
    } else {
      return (
        <React.Fragment>
          <div className="col-lg-8">
            <Chart labels={this.state.series} data={this.state.series} />
          </div>
        </React.Fragment>
      );
    }
  }
  render() {
    return (
      <div>
        <h1>Strona w budowie</h1>
        <p>blabal</p>
      </div>
    );
  }
}
