import React, { Component } from "react";
import ProgressExercise from "./ProgressExercise";
import ProgressParts from "./ProgressParts";

export class Progress extends Component {
  static displayName = Progress.name;

  constructor(props) {
    super(props);
    this.state = { loading: true };
  }

  renderBody() {
    return (
      <React.Fragment>
        <div className="col-12">
          <ProgressExercise />
          <ProgressParts />
        </div>
      </React.Fragment>
    );
  }

  render() {
    return this.renderBody();
  }
}
