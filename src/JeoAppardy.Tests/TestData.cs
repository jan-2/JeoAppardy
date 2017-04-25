
using System;

namespace JeoAppardy.Tests
{
  public class TestData
  {
    public static String OneBoard = @"
      {
        'categories':[
          {
            'title': 'Category 1',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 2',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 3',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 4',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          }]
      }";

    public static String FirstBoard = @"
      {
        'categories':[
          {
            'title': 'Category 1',
            'answers':[
              {
                'description': 'description 1.1',
                'related_question': 'question 1.1'
              },
              {
                'description': 'description 1.2',
                'related_question': 'question 1.2'
              },
              {
                'description': 'description 1.3',
                'related_question': 'question 1.3'
              },
              {
                'description': 'description 1.4',
                'related_question': 'question 1.4'
              }
            ]
          },
          {
            'title': 'Category 2',
            'answers':[
              {
                'description': 'description 2.1',
                'related_question': 'question 2.1'
              },
              {
                'description': 'description 2.2',
                'related_question': 'question 2.2'
              },
              {
                'description': 'description 2.3',
                'related_question': 'question 2.3'
              },
              {
                'description': 'description 2.4',
                'related_question': 'question 2.4'
              }
            ]
          },
          {
            'title': 'Category 3',
            'answers':[
              {
                'description': 'description 3.1',
                'related_question': 'question 3.1'
              },
              {
                'description': 'description 3.2',
                'related_question': 'question 3.2'
              },
              {
                'description': 'description 3.3',
                'related_question': 'question 3.3'
              },
              {
                'description': 'description 3.4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 4',
            'answers':[
              {
                'description': 'description 4.1',
                'related_question': 'question 4.1'
              },
              {
                'description': 'description 4.2',
                'related_question': 'question 4.2'
              },
              {
                'description': 'description 4.3',
                'related_question': 'question 4.3'
              },
              {
                'description': 'description 4.4',
                'related_question': 'question 4.4'
              }
            ]
          }]
      }";
    public static String SecondBoard = @"
      {
        'categories':[
          {
            'title': 'Category 1.2',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 2.2',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 3.2',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 4.2',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          }]
      }";
    public static String ThirdBoard = @"
      {
        'categories':[
          {
            'title': 'Category 1.3',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 2.3',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 3.3',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 4.3',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          }]
      }";
    public static String FourthBoard = @"
      {
        'categories':[
          {
            'title': 'Category 1.4',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 2.4',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 3.4',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 4.4',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          }]
      }";
    public static String FinalBoard = @"
      {
        'categories':[
          {
            'title': 'Category 1.5',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 2.5',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 3.5',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          },
          {
            'title': 'Category 4.5',
            'answers':[
              {
                'description': 'description 1',
                'related_question': 'question 1'
              },
              {
                'description': 'description 2',
                'related_question': 'question 2'
              },
              {
                'description': 'description 3',
                'related_question': 'question 3'
              },
              {
                'description': 'description 4',
                'related_question': 'question 4'
              }
            ]
          }]
      }";
  }
}
