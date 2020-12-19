import gql from 'graphql-tag';

export const LOGIN_QUERY = gql`
    query($input: UserInput!) {
        login(input: $input) {
            token
            userName
            id
        }
    }
`;