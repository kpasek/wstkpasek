import React, { Component } from "react";
import { Link } from "react-router-dom";
import "../css/site.css";

export class Schedule extends Component {
  static displayName = Schedule.name;

  constructor(props) {
    super(props);
    this.state = {
      year: new Date().getFullYear(),
      month: new Date().getMonth(),
      months: [
        "styczeń",
        "luty",
        "marzec",
        "kwiecień",
        "maj",
        "czerwiec",
        "lipiec",
        "sierpień",
        "wrzesień",
        "październik",
        "listopad",
        "grudzień",
      ],
      loading: true,
    };
  }
  componentDidMount() {
    this.fetchSchedule();
  }
  fetchSchedule = async () => {
    const scheduleRespone = await fetch(
      "api/schedule/trainings/" +
        this.state.year +
        "/" +
        (this.state.month + 1),
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
    const responseTrainings = await fetch("api/trainings", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const responseParts = await fetch("api/exercises/parts", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const responseTypes = await fetch("api/exercises/types", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const trainings = await responseTrainings.json();
    const schedule = await scheduleRespone.json();
    const parts = await responseParts.json();
    const types = await responseTypes.json();
    this.setState({
      schedule: schedule,
      trainings: trainings,
      parts: parts,
      types: types,
      loading: false,
    });
  };
  handleChangeMonth = async (change) => {
    this.state.month = (this.state.month + change) % 12;
    if (this.state.month === -1 || (this.state.month === 0 && change > 0)) {
      this.state.year += change;
    }

    if (this.state.month === -1) {
      this.state.month = 11;
    }

    const scheduleRespone = await fetch(
      "api/schedule/trainings/" +
        this.state.year +
        "/" +
        (this.state.month + 1),
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
    const schedule = await scheduleRespone.json();
    this.setState({
      schedule: schedule,
      year: this.state.year,
      month: this.state.month,
    });
  };
  handleAddTraining = async () => {
    const date = document.getElementById("add-training-date").value;
    const trainingId = parseInt(
      document.getElementById("select-training").value
    );
    const addResponse = await fetch("api/schedule/trainings", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        trainingId: trainingId,
        trainingDate: date,
      }),
    });
    const newTraining = await addResponse.json();
    this.state.schedule.push(newTraining);
    this.setState({
      schedule: this.state.schedule.sort((a, b) => {
        return a.trainingDate >= b.trainingDate ? 1 : -1;
      }),
      year: new Date().getFullYear(),
      month: new Date().getMonth(),
    });
  };
  renderPartIdList = () => {
    if (this.state.parts.length > 0) {
      return (
        <datalist id="parts-list">
          {this.state.parts.map((part) => (
            <option key={"part-key-" + part.name} value={part.name}>
              {part.name}
            </option>
          ))}
        </datalist>
      );
    } else {
      return (
        <datalist id="parts-list">
          <option key={"no-parts"} value="">
            Brak danych
          </option>
        </datalist>
      );
    }
  };
  renderStartButton(training) {
    if (!training.finish) {
      return (
        <Link
          to={{
            pathname: "/start/" + training.scheduleTrainingId,
            scheduleTrainingId: training.scheduleTrainingId,
          }}
        >
          <div className="float-right training-title pt-3 link-green">
            Start
            <i className="icon-play-outline" />
          </div>
        </Link>
      );
    }
  }
  renderTrainingLink(training) {
    if (training.finish) {
      return <div className="training-title link-green">{training.name}</div>;
    } else {
      return <div className="training-title link-black">{training.name}</div>;
    }
  }
  renderTrainings() {
    if (this.state.schedule.length > 0) {
      return (
        <React.Fragment>
          {this.state.schedule.map((training) => (
            <span key={training.scheduleTrainingId}>
              <div className="my-2 float-left">
                <Link to={"/harmonogram/" + training.scheduleTrainingId}>
                  {this.renderTrainingLink(training)}
                </Link>
                <div className="training-subtitle">
                  {new Date(training.trainingDate).toLocaleString()}
                </div>
              </div>
              {this.renderStartButton(training)}
              <div style={{ clear: "both" }}></div>
            </span>
          ))}
        </React.Fragment>
      );
    } else {
      return (
        <React.Fragment>
          <h3>Brak treningów w wybranym okresie</h3>
        </React.Fragment>
      );
    }
  }
  renderTrainingOptions() {
    return (
      <React.Fragment>
        {this.state.trainings.map((training) => (
          <option key={training.trainingId} value={training.trainingId}>
            {training.name}
          </option>
        ))}
      </React.Fragment>
    );
  }
  renderBody = () => {
    if (this.state.loading) {
      return <h3>Trwa ładowanie</h3>;
    }
    return (
      <React.Fragment>
        <div className="container">
          <div className="row">
            <div className="col-12 text-center">
              <h1 className="mt-3 mb-1 text-center">Harmonogram</h1>
              <div className="mb-3 font-size-20">
                <i
                  className="icon-left-open-big"
                  onClick={() => {
                    this.handleChangeMonth(-1);
                  }}
                />
                <span className="mx-2">
                  {this.state.months[this.state.month] +
                    " - " +
                    this.state.year}
                </span>
                <i
                  className="icon-right-open-big"
                  onClick={() => {
                    this.handleChangeMonth(1);
                  }}
                />
              </div>
            </div>
            <div className="col-lg-8 px-1">{this.renderTrainings()}</div>
            <div className="col-lg-4 mt-3">
              <h3 className="mt-3">Dodaj trening</h3>
              <select id="select-training" className="custom-select my-1">
                {this.renderTrainingOptions()}
              </select>
              <input
                type="datetime-local"
                id="add-training-date"
                className="form-control"
                defaultValue={new Date().toISOString().substr(0, 16)}
              />
              <button
                className="btn btn-outline-success mt-2"
                onClick={this.handleAddTraining}
              >
                Dodaj
              </button>
            </div>
          </div>
        </div>
      </React.Fragment>
    );
  };
  render() {
    return <React.Fragment>{this.renderBody()}</React.Fragment>;
  }
}
