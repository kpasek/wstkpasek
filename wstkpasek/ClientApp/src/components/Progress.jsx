import React, { Component } from "react";
import ProgressExercise from "./ProgressExercise";
import ProgressParts from "./ProgressParts";
import ProgressWeights from "./ProgressWeights";

export class Progress extends Component {
  static displayName = Progress.name;

  constructor(props) {
    super(props);
    this.state = { loading: true };
  }

  renderBody() {
    return (
      <React.Fragment>
        <div className="col-12 pb-5">
          <ProgressExercise />
          <ProgressParts />
          <ProgressWeights />
        </div>
      </React.Fragment>
    );
  }

  render() {
    return this.renderBody();
  }
}
