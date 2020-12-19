import gql from 'graphql-tag';

export const CREATE_GAME = gql`
    mutation($input: GameInput!) {
        createGame(input: $input) {
            id
            name
        }
    }
`;

export const UPDATE_GAME = gql`
    mutation($id: ID!, $input: GameInput!) {
        updateGame(id: $id, input: $input) {
            id
            name
            startTime
            link
            playersNumber
            gettedNumbers
            finished
        }
    }
`;

export const DELETE_GAME = gql`
    mutation($id: ID!) {
        deleteGame(id: $id) {
            id
        }
    }
`;


