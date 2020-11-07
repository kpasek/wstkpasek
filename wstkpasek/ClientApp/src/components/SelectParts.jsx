import React, { Component } from "react";

export default class SelectParts extends Component {
  constructor(props) {
    super(props);

    this.state = {
      selectId: this.props.selectId,
      default: this.props.default,
      parts: [],
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
    this.setState({
      parts: parts,
    });
  };
  async shouldComponentUpdate() {
    if (this.props.reload) {
      await this.fetchData();
      return true;
    }
  }
  renderDefault() {
    if (this.state.default) {
      return (
        <option key="select-default" value="all">
          Wybierz partię
        </option>
      );
    }
  }
  renderPartsOptions = () => {
    return (
      <React.Fragment>
        {this.renderDefault()}
        {this.state.parts.map((part) => (
          <option key={"select-" + part.name} value={part.name}>
            {part.name}
          </option>
        ))}
      </React.Fragment>
    );
  };

  render() {
    const id =
      this.state.selectId === undefined ? "select-parts" : this.state.selectId;
    return (
      <React.Fragment>
        <label htmlFor={id}>Wybierz partię:</label>
        <select
          className="custom-select my-2"
          id={id}
          onChange={this.props.onChangePart}
        >
          {this.renderPartsOptions()}
        </select>
      </React.Fragment>
    );
  }
}
