import React, { Component } from "react";

export class Progress extends Component {
  static displayName = Progress.name;

  constructor(props) {
    super(props);
    this.state = { loading: true };
  }

  componentDidMount() {}

  render() {
    return (
      <div>
        <h1>Strona w budowie</h1>
        <p>Ta część serwisu nie została jeszcze ukończona</p>
      </div>
    );
  }
}
