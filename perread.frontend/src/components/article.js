import { Component } from "react";
import { useParams } from 'react-router-dom';
import { useFetchWrapper } from "../_helpers/fetch-wrapper";

export {useRenderArticle}

async function useRenderArticle() {
    const params = useParams();

    const fetchWrapper = useFetchWrapper();
    
    const response = await fetchWrapper.get(`https://localhost:7176/article/${params.id}`);
    const data = response.json();

    return (
        <div>{data}</div>
    );
}

// export class Article extends Component 
// { 
//     constructor(props) {
//         super(props);
//         this.state = { id: this.props.match.params.id, article: null, isLoading: true };
//     }

//     componentDidMount() {
//         this.refreshData();
//     }

//     render() {
//         if (this.state.isLoading) {
//             return(
//                 <div>Loading...</div>
//             );
//         }

//         return(
//             <div>
//                 {/* Random */}
//                 {this.state.article.content}
//             </div>
//         );
//     }

//     async refreshData() {
//         const fetchWrapper = useFetchWrapper();
//         const response = await fetchWrapper.get(`https://localhost:7176/article/${this.state.id}`);
//         const data = await response.json();
//         this.setState({article: data, isLoading: false});
//     }
// }

// export default withRouter(Article);