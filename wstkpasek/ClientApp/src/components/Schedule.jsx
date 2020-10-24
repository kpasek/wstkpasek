import React, { Component } from "react";
import { Link } from "react-router-dom";
import "../css/site.css";

export class Schedule extends Component {
  static displayName = Schedule.name;

  constructor(props) {
    super(props);
    this.state = {
      year: 2020,
      month: 10,
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
    const today = new Date();
    const month = today.getMonth() + 1;
    const scheduleRespone = await fetch(
      "api/schedule/trainings/" + today.getFullYear() + "/" + month,
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
  renderBody = () => {
    if (this.state.loading) {
      return <h3>Trwa ładowanie</h3>;
    }
    return (
      <React.Fragment>
        <div className="">
          <div className="col-lg-8 text-center">
            <h1 className="mt-3 mb-1 text-center">Harmonogram</h1>
            <div className="mb-3 font-size-20">
              <i className="icon-left-open-big" />
              <span className="mx-2">
                {this.state.months[this.state.month] + " - " + this.state.year}
              </span>
              <i className="icon-right-open-big" />
            </div>
          </div>
          <div className="col-lg-8 px-1">
            {this.state.schedule.map((training) => (
              <span key={training.scheduleTrainingId}>
                <div className="my-2 float-left">
                  <Link
                    to={{
                      pathname: "/harmonogram/" + training.scheduleTrainingId,
                      scheduleTrainingId: training.scheduleTrainingId,
                    }}
                  >
                    <div className="training-title link-black">
                      {training.name}
                    </div>
                  </Link>
                  <div className="training-subtitle">
                    {new Date(training.trainingDate).toLocaleString()}
                  </div>
                </div>
                <Link
                  to={{
                    pathname: "/start/" + training.scheduleTrainingId,
                    scheduleTrainingId: training.scheduleTrainingId,
                  }}
                >
                  <div className="float-right training-title pt-3 link-black">
                    Start
                    <i className="icon-play-outline" />
                  </div>
                </Link>
                <div style={{ clear: "both" }}></div>
              </span>
            ))}
          </div>
        </div>
      </React.Fragment>
    );
  };
  render() {
    return <React.Fragment>{this.renderBody()}</React.Fragment>;
  }
}
