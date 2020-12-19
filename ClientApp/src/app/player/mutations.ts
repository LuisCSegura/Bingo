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
export const JOIN_A_GAME = gql`
mutation($link: String!){
    joinAGame(link:$link){
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
export const QUIT_THE_GAME = gql`
mutation($id: ID!){
    quitTheGame(id:$id){
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
