import React, { Component } from "react";
import { Training } from "./Training";

import "../../css/site.css";

export class Trainings extends Component {
  static displayName = Trainings.name;

  constructor(props) {
    super(props);
    this.state = { trainings: [], parts: [], types: [], loading: true };
  }

  async componentDidMount() {
    await this.fetchTrainingData();
  }

  async fetchTrainingData() {
    const response = await fetch("api/trainings", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const data = await response.json();
    const responseParts = await fetch("api/exercises/parts", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const parts = await responseParts.json();
    const responseTypes = await fetch("api/exercises/types", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const types = await responseTypes.json();
    this.setState({
      trainings: data,
      parts: parts,
      types: types,
      loading: false,
    });
  }
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
  renderTypeIdList = () => {
    if (this.state.types.length > 0) {
      return (
        <datalist id="types-list">
          {this.state.types.map((type) => (
            <option key={"type-key-" + type.typeName} value={type.typeName}>
              {type.typeName}
            </option>
          ))}
        </datalist>
      );
    } else {
      return (
        <datalist id="types-list">
          <option key={"no-types"} value="">
            Brak danych
          </option>
        </datalist>
      );
    }
  };
  handleNewTraining = async () => {
    const name = document.getElementById("new-training-name").value;
    const exerciseNumber = document.getElementById("new-training-exercises")
      .value;

    await fetch("api/trainings", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name: name,
        exerciseNumber: parseInt(exerciseNumber),
      }),
    });
    await this.fetchTrainingData();
  };
  handleDeleteTraining = async (trainingId) => {
    await fetch("api/trainings/" + trainingId, {
      method: "DELETE",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    this.setState({
      trainings: this.state.trainings.filter(
        (tr) => tr.trainingId !== trainingId
      ),
    });
  };
  renderBody() {
    if (this.state.loading) return <h2>Trwa ładowanie...</h2>;
    return (
      <React.Fragment>
        <div className="col-lg-8 mx-auto">
          {/* new training button */}
          <i
            className="icon-plus font-size-20 float-left pl-3 pt-2"
            type="button"
            data-toggle="collapse"
            data-target={"#new-training"}
            aria-expanded="false"
            aria-controls={"new-training"}
          ></i>
          <h1 className="mt-3 mb-4 text-center">Treningi</h1>
          <div style={{ clear: "both" }}></div>
          {/* new training fields */}
          <div
            id={"new-training"}
            className="collapse"
            aria-labelledby="newtraining"
          >
            <h3>Nowy trening</h3>
            <label
              htmlFor="new-training-exercises"
              className="font-size-18 mt 2"
            >
              Nazwa treningu
            </label>
            <input
              type="text"
              className="form-control mt-1"
              id="new-training-name"
              required
            ></input>
            <label
              htmlFor="new-training-exercises"
              className="font-size-18 mt 2"
            >
              Ilość jednocześnie wykonywanych ćwiczeń
            </label>
            <input
              type="number"
              className="form-control mt-1"
              id="new-training-exercises"
              min="1"
              defaultValue="2"
            ></input>
            <button
              className="btn btn-outline-primary mt-2"
              onClick={this.handleNewTraining}
            >
              Zatwierdź
            </button>
          </div>
          <div className="mt-3 p-0">
            {this.state.trainings.map((training) => (
              <Training
                key={training.trainingId}
                trainingId={training.trainingId}
                onDeleteTraining={this.handleDeleteTraining}
                types={this.state.types}
                parts={this.state.parts}
              />
            ))}
            {this.renderTypeIdList()}
            {this.renderPartIdList()}
          </div>
        </div>
      </React.Fragment>
    );
  }
  render() {
    return this.renderBody();
  }
}
