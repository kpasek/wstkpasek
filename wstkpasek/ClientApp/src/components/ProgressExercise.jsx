import React, { Component } from "react";
import Chart from "./Chart";
import SelectParts from "./SelectParts";

export default class ProgressExercise extends Component {
  constructor(props) {
    super(props);
    this.state = {
      progressPart: [],
      selectId: "select-part-exercise",
      chartId: "progressChartByexercise",
      dateFromId: "dateFromId",
      dateToId: "dateToId",
      selectExerciseId: "select-exercise",
      loadChart: true,
      loading: true,
    };
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
    const exercisesResponse = await fetch(
      "api/exercises/part/" + parts[0].name,
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
    const exercises = await exercisesResponse.json();
    const progressResponse = await fetch(
      "api/progress/exercise/" + exercises[0].exerciseId,
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

    const series = await progressResponse.json();
    this.setState({
      progressExercise: series,
      parts: parts,
      exercises: exercises,
      loading: false,
    });
  };
  handleChangeExercise = async () => {
    this.setState({
      loadChart: false,
    });
    const exercise = document.getElementById(this.state.selectExerciseId).value;
    const dateFrom = document.getElementById(this.state.dateFromId).value;
    const dateTo = document.getElementById(this.state.dateToId).value;
    const progressResponse = await fetch(
      "api/progress/exercise/" +
        exercise +
        "?dateFrom=" +
        dateFrom +
        "&dateTo=" +
        dateTo,
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
    const progress = await progressResponse.json();
    this.setState({
      progressExercise: progress,
      loadChart: true,
    });
  };
  handleChangePart = async () => {
    const part = document.getElementById(this.state.selectId).value;
    this.setState({
      loadChart: false,
    });

    const dateFrom = document.getElementById(this.state.dateFromId).value;
    const dateTo = document.getElementById(this.state.dateToId).value;
    const exercisesResponse = await fetch("api/exercises/part/" + part, {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const exercises = await exercisesResponse.json();
    const progressResponse = await fetch(
      "api/progress/exercise/" +
        exercises[0].exerciseId +
        "?dateFrom=" +
        dateFrom +
        "&dateTo=" +
        dateTo,
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
    const progress = await progressResponse.json();
    this.setState({
      progressExercise: progress,
      exercises: exercises,
      loadChart: true,
    });
  };
  prepareDates() {
    let dates = [];
    for (let i = 0; i < this.state.progressExercise.length; i++) {
      dates.push(this.state.progressExercise[i].date);
    }
    return dates;
  }
  prepareDataSet() {
    let loads = [];
    let times = [];
    let distances = [];
    let resttimes = [];
    let repeats = [];
    for (let i = 0; i < this.state.progressExercise.length; i++) {
      loads.push(this.state.progressExercise[i].loadAv);
      times.push(this.state.progressExercise[i].timeAv);
      distances.push(this.state.progressExercise[i].distanceAv);
      repeats.push(this.state.progressExercise[i].repeatAv);
      resttimes.push(this.state.progressExercise[i].restTimeAv);
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
            this.state.progressExercise[0] !== undefined
              ? "Średnie wartości w dniach - " +
                this.state.progressExercise[0].type
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
  renderSelectExercise() {
    return (
      <React.Fragment>
        <select
          className="custom-select"
          id={this.state.selectExerciseId}
          onChange={this.handleChangeExercise}
        >
          {this.state.exercises.map((exercise) => (
            <option key={exercise.exerciseId} value={exercise.exerciseId}>
              {exercise.name}
            </option>
          ))}
        </select>
      </React.Fragment>
    );
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
              <h3>Statystyki dla ćwiczenia</h3>
            </div>
            <SelectParts
              onChangePart={this.handleChangePart}
              default={false}
              selectId={this.state.selectId}
            />
            <label htmlFor={this.state.selectExerciseId}>
              Wybierz ćwiczenie
            </label>
            {this.renderSelectExercise()}
            <div className="col">
              <label htmlFor={this.state.dateFromId}>Okres od: </label>
              <input
                type="date"
                className="form-control"
                id={this.state.dateFromId}
                onChange={this.handleChangeExercise}
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
                onChange={this.handleChangeExercise}
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
