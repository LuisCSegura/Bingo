import gql from 'graphql-tag';

export const GAMES_QUERY = gql`
query($name: String, $startTime: DateTime, $link: String, $playersNumber: Int, $gettedNumber: Int, $finished: Boolean){
    games(name:$name, startTime:$startTime, link:$link, playersNumber:$playersNumber, gettedNumber:$gettedNumber, finished:$finished){
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
export const GAME_QUERY = gql`
query($id: ID!){
    game(id:$id){
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