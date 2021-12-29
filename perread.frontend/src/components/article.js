import { Component } from "react";

export class Article extends Component 
{ 
    constructor(props) {
        super(props);
        this.state = { id: this.props.match.params.id, article: null, isLoading: true };
    }

    componentDidMount() {
        this.refreshData();
    }

    render() {
        if (this.state.isLoading) {
            return(
                <div>Loading...</div>
            );
        }

        return(
            <div>
                {/* Random */}
                {this.state.article.content}
            </div>
        );
    }

    async refreshData() {
        const response = await fetch(`https://localhost:7176/article/${this.state.id}`);
        const data = await response.json();
        this.setState({article: data, isLoading: false});
    }
}

// export default withRouter(Article);