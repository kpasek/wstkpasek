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
    const response = await fetch("/api/trainings", {
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
  renderBody() {
    if (this.state.loading) return <h2>Trwa ładowanie...</h2>;
    return (
      <React.Fragment>
        <div className="mt-3 col-lg-8 p-0">
          {this.state.trainings.map((training) => (
            <Training
              key={training.trainingId}
              trainingId={training.trainingId}
              types={this.state.types}
              parts={this.state.parts}
            />
          ))}
          {this.renderTypeIdList()}
          {this.renderPartIdList()}
        </div>
      </React.Fragment>
    );
  }
  render() {
    return this.renderBody();
  }
}
