import React, { Component } from "react";

export class Privacy extends Component {
  static displayName = Privacy.name;

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
