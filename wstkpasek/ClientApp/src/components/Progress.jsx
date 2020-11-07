import React, { Component } from "react";
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
        <ProgressParts />
      </React.Fragment>
    );
  }

  render() {
    return this.renderBody();
  }
}
