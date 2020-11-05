import React, { Component } from "react";
import Chart from "chart.js";

export default class ProgressChart extends Component {
  constructor(props) {
    super(props);
    this.state = {
      labels: this.props.dataSet.labels,
      data: this.props.dataSet.data,
      type: this.props.dataSet.dataType,
      loading: true,
    };
  }
  componentDidMount() {
    const chart = document.getElementById("myChart");
    const maxOffset = Math.max(...this.state.data) * 1.1;
    const minOffset = Math.min(...this.state.data) * 0.9;
    console.log(minOffset, maxOffset)
    let myChart = new Chart(chart, {
      type: "line",
      data: {
        labels: this.state.labels,
        datasets: [
          {
            label: this.state.type,
            data: this.state.data,
            borderColor: ["rgba(255, 99, 132, 1)"],
            borderWidth: 1,
          },
          {
            label: this.state.type,
            data: this.state.data,
            borderColor: ["rgba(255, 99, 132, 1)"],
            borderWidth: 1,
          },
        ],
      },
      options: {
        responsive: true, // Instruct chart js to respond nicely.
        maintainAspectRatio: true,
        scales: {
          yAxes: [
            {
              ticks: {
                suggestedMin: minOffset,
                suggestedMax: maxOffset,
                beginAtZero: false,
              },
            },
          ],
        },
      },
    });
  }
  renderChart() {
    return <React.Fragment></React.Fragment>;
  }
  setChartProps() {
    if (!this.state.loading) {
      return <h4>Trwa ładowanie wykresu</h4>;
    } else {
    }
  }
  renderBody() {
    return (
      <React.Fragment>
        {this.renderChart()}
        {this.setChartProps()}
      </React.Fragment>
    );
  }
  render() {
    return <React.Fragment>{this.renderBody()}</React.Fragment>;
  }
}
