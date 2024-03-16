import { InputAdornment, TextField } from "@mui/material";

function round2Decimals(value) {
    let x = +value;
    x *= 100;
    x = Math.round(x);
    x /= 100;
    return x;
}
  
function Inputm2(params) {
  return  <TextField
      InputProps={{
      endAdornment: <InputAdornment position="end">m<sup>2</sup></InputAdornment>,
      disableUnderline: true,
      inputProps: {style: { textAlign: "right" },}
    }}
    fullWidth
    variant="standard"
    value={params.value}
  />;
}

function InputEUR(params) {
  return  <TextField
    InputProps={{
      endAdornment: <InputAdornment position="end">€</InputAdornment>,
      disableUnderline: true,
      inputProps: {style: { textAlign: "right", width:'100%' },}
    }}
    fullWidth
    variant="standard"
    value={params.value}
  />;
}

function InputRSD(params) {
  return  <TextField
    InputProps={{
    endAdornment: <InputAdornment position="end">RSD</InputAdornment>,
    disableUnderline: true,
    inputProps: {style: { textAlign: "right" },}
    }}
    fullWidth
    variant="standard"
    value={params.value}
  />;  
}

export function getStanColumns(RSD4EUR) {
    const columns = [
        { 
            field: 'broj', 
            headerName: 'Broj', 
            align: 'right',
            editable: true,
        },
        { 
            field: 'brojListaNepokretnosti', 
            headerName: 'Broj LN', 
            align: 'right',
            editable: true, 
        },
        { 
          field: 'brojSoba', 
          headerName: 'Broj soba', 
          align: 'right',
          flex: 1,
          editable: true, 
        },    
        { 
            field: 'povrsina', 
            headerName: 'Povrsina', 
            type: 'number',
            renderCell: Inputm2,
            editable: true,
        },
        { 
            field: 'cenaKv', 
            headerName: 'Cena po kvadratu', 
            type: 'number',
            renderCell: InputEUR,
            renderHeader: () => <></>,
            editable: true,
        },
        { 
            field: 'cenaKvPDV', 
            headerName: 'Cena po kvadratu sa PDV-om', 
            type: 'number',
            renderCell: InputEUR,
            renderHeader: () => <></>,
            editable: true, 
            valueGetter: (params) => {
              let withoutPDV = +params.row.cenaKv;
              let withPDV = withoutPDV * 1.2;
              return round2Decimals(withPDV);
            },
            valueSetter: (params) => {
              let withPDV = +params.value.toString();
              let withoutPDV = withPDV / 1.2;
              return { ...params.row, cenaKv: round2Decimals(withoutPDV) };
            },
        },
        { 
            field: 'cenaUk', 
            headerName: 'Cena ukupno', 
            type: 'number',
            renderCell: InputEUR,
            renderHeader: () => <></>,
            editable: true, 
            valueGetter: (params) => {
              let withoutPDV = +params.row.cenaKv;
              let pov = +params.row.povrsina;
              let uk = withoutPDV * pov;
              return round2Decimals(uk);
            },
            valueSetter: (params) => {
              let withoutPDV = +params.value;
              let pov = +params.row.povrsina;
              return { ...params.row, cenaKv: round2Decimals(withoutPDV / pov) };
            }
        },
        { 
            field: 'cenaUkPDV', 
            headerName: 'Cena ukupno sa PDV-om', 
            type: 'number',
            renderCell: InputEUR,
            renderHeader: () => <></>,
            editable: true, 
            valueGetter: (params) => {
              let withoutPDV = +params.row.cenaKv;
              let pov = +params.row.povrsina;
              let uk = withoutPDV * pov;
              return round2Decimals(uk * 1.2);
            },
            valueSetter: (params) => {
              let withPDV = +params.value.toString();
              let withoutPDV = withPDV / 1.2;
              let pov = +params.row.povrsina;
              return { ...params.row, cenaKv: round2Decimals(withoutPDV / pov) };
            },
        },
        { 
            field: 'cenaKvRSD', 
            headerName: 'Cena po kvadratu u RSD', 
            type: 'number',
            renderCell: InputRSD,
            renderHeader: () => <></>,
            editable: true,
            valueGetter: (params) => {
                let cenaEUR = +params.row.cenaKv;
                let cenaRSD = cenaEUR * RSD4EUR;
                return round2Decimals(cenaRSD);
              },
              valueSetter: (params) => {
                let cenaRSD = +params.value.toString();
                let cenaEUR = cenaRSD / RSD4EUR;
                return { ...params.row, cenaKv: round2Decimals(cenaEUR) };
              },
          },
        { 
            field: 'cenaKvPDVRSD', 
            headerName: 'Cena po kvadratu sa PDV-om u RSD', 
            type: 'number',
            renderCell: InputRSD,
            renderHeader: () => <></>,
            editable: true, 
            valueGetter: (params) => {
              let withoutPDV = +params.row.cenaKv;
              let withPDV = withoutPDV * 1.2;
              return round2Decimals(withPDV * RSD4EUR);
            },
            valueSetter: (params) => {
              let withPDV = +params.value.toString();
              let withoutPDV = withPDV / 1.2;
              return { ...params.row, cenaKv: round2Decimals(withoutPDV / RSD4EUR) };
            },
        },
        { 
            field: 'cenaUkRSD', 
            headerName: 'Cena ukupno u RSD', 
            type: 'number',
            renderCell: InputRSD,
            renderHeader: () => <></>,
            editable: true, 
            valueGetter: (params) => {
              let withoutPDV = +params.row.cenaKv;
              let pov = +params.row.povrsina;
              let uk = withoutPDV * pov;
              return round2Decimals(uk * RSD4EUR);
            },
            valueSetter: (params) => {
              let withoutPDV = +params.value;
              let pov = +params.row.povrsina;
              return { ...params.row, cenaKv: round2Decimals(withoutPDV / pov / RSD4EUR) };
            }
        },
        { 
            field: 'cenaUkPDVRSD', 
            headerName: 'Cena ukupno sa PDV-om u RSD', 
            type: 'number',
            renderCell: InputRSD,
            renderHeader: () => <></>,
            editable: true, 
            valueGetter: (params) => {
              let withoutPDV = +params.row.cenaKv;
              let pov = +params.row.povrsina;
              let uk = withoutPDV * pov;
              return round2Decimals(uk * 1.2 * RSD4EUR);
            },
            valueSetter: (params) => {
              let withPDV = +params.value.toString();
              let withoutPDV = withPDV / 1.2;
              let pov = +params.row.povrsina;
              return { ...params.row, cenaKv: round2Decimals(withoutPDV / pov / RSD4EUR) };
            },
        },        
        { 
            field: 'sprat', 
            headerName: 'Sprat', 
            align: 'right',
            type: 'text',
            editable: true, 
        },
        { 
            field: 'brojUlaza', 
            headerName: 'Broj Ulaza', 
            align: 'right',
            type: 'text',
            editable: true, 
        },
        { 
            field: 'orijentacija', 
            headerName: 'Orijentacija', 
            type: 'text',
            editable: true, 
        },
        { 
            field: 'opis', 
            headerName: 'Opis', 
            type: 'text',
            editable: true, 
        },
    ];

    return columns;
}

function getStanColumnGroupsValutaPDVJM() {
    const columnGroupingModel = [
        {
          groupId: 'EUR',
          headerName: 'EUR',
          description: '',
          headerAlign: 'center',
          freeReordering: true,
          children: [
            {
              groupId: 'EURwithoutPDV',
              headerName: 'Bez PDV-a',
              description: '',
              headerAlign: 'center',
              freeReordering: true,
              children: [
                {
                    groupId: 'EURwithoutPDVKv',
                    headerName: 'Kvadrat',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaKv' }]      
                },
                {
                    groupId: 'EURwithoutPDVUk',
                    headerName: 'Ukupno',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaUk' }]      
                }
              ]
            },
            {
              groupId: 'EURwithPDV',
              headerName: 'Sa PDV-om',
              description: '',
              headerAlign: 'center',
              freeReordering: true,
              children: [
                {
                    groupId: 'EURwithPDVKv',
                    headerName: 'Kvadrat',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaKvPDV' }]
                },
                {
                    groupId: 'EURwithPDVUk',
                    headerName: 'Ukupno',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaUkPDV' }]
                }
              ]
            },
          ]
        },
        {
            groupId: 'RSD',
            headerName: 'RSD',
            description: '',
            headerAlign: 'center',
            freeReordering: true,
            children: [
              {
                groupId: 'RSDwithoutPDV',
                headerName: 'Bez PDV-a',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                  {
                      groupId: 'RSDwithoutPDVKv',
                      headerName: 'Kvadrat',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaKvRSD' }]      
                  },
                  {
                      groupId: 'RSDwithoutPDVUk',
                      headerName: 'Ukupno',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaUkRSD' }]      
                  }
                ]
              },
              {
                groupId: 'RSDwithPDV',
                headerName: 'Sa PDV-om',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                  {
                      groupId: 'RSDwithPDVKv',
                      headerName: 'Kvadrat',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaKvPDVRSD' }]
                  },
                  {
                      groupId: 'RSDwithPDVUk',
                      headerName: 'Ukupno',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaUkPDVRSD' }]
                  }
                ]
              },
            ]
          },    ];

    return columnGroupingModel;
}

function getStanColumnGroupsValutaJMPDV() {
    const columnGroupingModel = [
        {
          groupId: 'EUR',
          headerName: 'EUR',
          description: '',
          headerAlign: 'center',
          freeReordering: true,
          children: [
            {
                groupId: 'EURKv',
                headerName: 'Kvadrat',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                    {
                        groupId: 'EURKvWithoutPDV',
                        headerName: 'Bez PDV-a',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKv'}]
                    },
                    {
                        groupId: 'EURKvWithPDV',
                        headerName: 'Sa PDV-om',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKvPDV'}]
                    }
                ]
            },
            {
                groupId: 'EURUk',
                headerName: 'Ukupno',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                    {
                        groupId: 'EURUkWithoutPDV',
                        headerName: 'Bez PDV-a',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaUk'}]
                    },
                    {
                        groupId: 'EURUkWithPDV',
                        headerName: 'Sa PDV-om',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaUkPDV'}]
                    }
                ]
            },
          ]
        },
        {
            groupId: 'RSD',
            headerName: 'RSD',
            description: '',
            headerAlign: 'center',
            freeReordering: true,
            children: [
              {
                  groupId: 'RSDKv',
                  headerName: 'Kvadrat',
                  description: '',
                  headerAlign: 'center',
                  freeReordering: true,
                  children: [
                      {
                          groupId: 'RSDKvWithoutPDV',
                          headerName: 'Bez PDV-a',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaKvRSD'}]
                      },
                      {
                          groupId: 'RSDKvWithPDV',
                          headerName: 'Sa PDV-om',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaKvPDVRSD'}]
                      }
                  ]
              },
              {
                  groupId: 'RSDUk',
                  headerName: 'Ukupno',
                  description: '',
                  headerAlign: 'center',
                  freeReordering: true,
                  children: [
                      {
                          groupId: 'RSDUkWithoutPDV',
                          headerName: 'Bez PDV-a',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUkRSD'}]
                      },
                      {
                          groupId: 'RSDUkWithPDV',
                          headerName: 'Sa PDV-om',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUkPDVRSD'}]
                      }
                  ]
              },
            ]
          },
        ];

    return columnGroupingModel;
}

function getStanColumnGroupsJMValutaPDV() {
    const columnGroupingModel = [
        {
          groupId: 'Kv',
          headerName: 'Kvadrat',
          description: '',
          headerAlign: 'center',
          freeReordering: true,
          children: [
            {
                groupId: 'KvEUR',
                headerName: 'EUR',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                    {
                        groupId: 'KvEURWithoutPDV',
                        headerName: 'Bez PDV-a',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKv'}]
                    },
                    {
                        groupId: 'KvEURWithPDV',
                        headerName: 'Sa PDV-om',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKvPDV'}]
                    }
                ]
            },
            {
                groupId: 'KvRSD',
                headerName: 'RSD',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                    {
                        groupId: 'KvRSDWithoutPDV',
                        headerName: 'Bez PDV-a',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKvRSD'}]
                    },
                    {
                        groupId: 'KvRSDWithPDV',
                        headerName: 'Sa PDV-om',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKvPDVRSD'}]
                    }
                ]
            },
          ]
        },
        {
            groupId: 'Uk',
            headerName: 'Ukupno',
            description: '',
            headerAlign: 'center',
            freeReordering: true,
            children: [
              {
                  groupId: 'UkEUR',
                  headerName: 'EUR',
                  description: '',
                  headerAlign: 'center',
                  freeReordering: true,
                  children: [
                      {
                          groupId: 'UkEURWithoutPDV',
                          headerName: 'Bez PDV-a',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUk'}]
                      },
                      {
                          groupId: 'UkEURWithPDV',
                          headerName: 'Sa PDV-om',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUkPDV'}]
                      }
                  ]
              },
              {
                  groupId: 'UkRSD',
                  headerName: 'RSD',
                  description: '',
                  headerAlign: 'center',
                  freeReordering: true,
                  children: [
                      {
                          groupId: 'UkRSDWithoutPDV',
                          headerName: 'Bez PDV-a',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUkRSD'}]
                      },
                      {
                          groupId: 'UkRSDWithPDV',
                          headerName: 'Sa PDV-om',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUkPDVRSD'}]
                      }
                  ]
              },
            ]
          },
        ];

    return columnGroupingModel;
}

function getStanColumnGroupsJMPDVValuta() {
    const columnGroupingModel = [
        {
          groupId: 'Kv',
          headerName: 'Kvadrat',
          description: '',
          headerAlign: 'center',
          freeReordering: true,
          children: [
            {
                groupId: 'KvWithoutPDV',
                headerName: 'Bez PDV-a',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                    {
                        groupId: 'KvWithoutPDVEUR',
                        headerName: 'EUR',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKv'}]
                    },
                    {
                        groupId: 'KvWithoutPDVRSD',
                        headerName: 'RSD',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKvRSD'}]
                    }
                ]
            },
            {
                groupId: 'KvWithPDV',
                headerName: 'Sa PDV-om',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                    {
                        groupId: 'KvWithPDVEUR',
                        headerName: 'EUR',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKvPDV'}]
                    },
                    {
                        groupId: 'KvWithPDVRSD',
                        headerName: 'RSD',
                        descripiton: '',
                        headerAlign: 'center',
                        freeReordering: true,
                        children: [{ field: 'cenaKvPDVRSD'}]
                    }
                ]
            },
          ]
        },
        {
            groupId: 'Uk',
            headerName: 'Ukupno',
            description: '',
            headerAlign: 'center',
            freeReordering: true,
            children: [
              {
                  groupId: 'UkWithoutPDV',
                  headerName: 'Bez PDV-a',
                  description: '',
                  headerAlign: 'center',
                  freeReordering: true,
                  children: [
                      {
                          groupId: 'UkWithoutPDVEUR',
                          headerName: 'EUR',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUk'}]
                      },
                      {
                          groupId: 'UkWithoutPDVRSD',
                          headerName: 'RSD',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUkRSD'}]
                      }
                  ]
              },
              {
                  groupId: 'UkWithPDV',
                  headerName: 'Sa PDV-om',
                  description: '',
                  headerAlign: 'center',
                  freeReordering: true,
                  children: [
                      {
                          groupId: 'UkWithPDVEUR',
                          headerName: 'EUR',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUkPDV'}]
                      },
                      {
                          groupId: 'UkWithPDVRSD',
                          headerName: 'RSD',
                          descripiton: '',
                          headerAlign: 'center',
                          freeReordering: true,
                          children: [{ field: 'cenaUkPDVRSD'}]
                      }
                  ]
              },
            ]
          },
        ];

    return columnGroupingModel;
}

function getStanColumnGroupsPDVValutaJM() {
    const columnGroupingModel = [
        {
          groupId: 'WithoutPDV',
          headerName: 'Bez PDV-a',
          description: '',
          headerAlign: 'center',
          freeReordering: true,
          children: [
            {
              groupId: 'WithoutPDVEUR',
              headerName: 'EUR',
              description: '',
              headerAlign: 'center',
              freeReordering: true,
              children: [
                {
                    groupId: 'WithoutPDVEURKv',
                    headerName: 'Kvadrat',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaKv' }]      
                },
                {
                    groupId: 'WithoutPDVEURUk',
                    headerName: 'Ukupno',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaUk' }]      
                }
              ]
            },
            {
              groupId: 'WithoutPDVRSD',
              headerName: 'RSD',
              description: '',
              headerAlign: 'center',
              freeReordering: true,
              children: [
                {
                    groupId: 'WithoutPDVRSDKv',
                    headerName: 'Kvadrat',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaKvRSD' }]
                },
                {
                    groupId: 'WithoutPDVRSDUk',
                    headerName: 'Ukupno',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaUkRSD' }]
                }
              ]
            },
          ]
        },
        {
            groupId: 'WithPDV',
            headerName: 'Sa PDV-om',
            description: '',
            headerAlign: 'center',
            freeReordering: true,
            children: [
              {
                groupId: 'WithPDVEUR',
                headerName: 'EUR',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                  {
                      groupId: 'WithPDVEURKv',
                      headerName: 'Kvadrat',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaKvPDV' }]      
                  },
                  {
                      groupId: 'WithPDVEURUk',
                      headerName: 'Ukupno',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaUkPDV' }]      
                  }
                ]
              },
              {
                groupId: 'WithPDVRSD',
                headerName: 'RSD',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                  {
                      groupId: 'WithPDVRSDKv',
                      headerName: 'Kvadrat',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaKvPDVRSD' }]
                  },
                  {
                      groupId: 'WithPDVRSDUk',
                      headerName: 'Ukupno',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaUkPDVRSD' }]
                  }
                ]
              },
            ]
          },    ];

    return columnGroupingModel;
}

function getStanColumnGroupsPDVJMValuta() {
    const columnGroupingModel = [
        {
          groupId: 'WithoutPDV',
          headerName: 'Bez PDV-a',
          description: '',
          headerAlign: 'center',
          freeReordering: true,
          children: [
            {
              groupId: 'WithoutPDVKv',
              headerName: 'Kvadrat',
              description: '',
              headerAlign: 'center',
              freeReordering: true,
              children: [
                {
                    groupId: 'WithoutPDVKvEUR',
                    headerName: 'EUR',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaKv' }]      
                },
                {
                    groupId: 'WithoutPDVKvRSD',
                    headerName: 'RSD',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaKvRSD' }]      
                }
              ]
            },
            {
              groupId: 'WithoutPDVUk',
              headerName: 'Ukupno',
              description: '',
              headerAlign: 'center',
              freeReordering: true,
              children: [
                {
                    groupId: 'WithoutPDVUkEUR',
                    headerName: 'EUR',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaUk' }]
                },
                {
                    groupId: 'WithoutPDVUkRSD',
                    headerName: 'RSD',
                    description: '',
                    headerAlign: 'center',
                    freeReordering: true,
                    children: [{ field: 'cenaUkRSD' }]
                }
              ]
            },
          ]
        },
        {
            groupId: 'WithPDV',
            headerName: 'Sa PDV-om',
            description: '',
            headerAlign: 'center',
            freeReordering: true,
            children: [
              {
                groupId: 'WithPDVKv',
                headerName: 'Kvadrat',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                  {
                      groupId: 'WithPDVKvEUR',
                      headerName: 'EUR',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaKvPDV' }]      
                  },
                  {
                      groupId: 'WithPDVKvRSD',
                      headerName: 'RSD',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaKvPDVRSD' }]      
                  }
                ]
              },
              {
                groupId: 'WithPDVUk',
                headerName: 'Ukupno',
                description: '',
                headerAlign: 'center',
                freeReordering: true,
                children: [
                  {
                      groupId: 'WithPDVUkEUR',
                      headerName: 'EUR',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaUkPDV' }]
                  },
                  {
                      groupId: 'WithPDVUkRSD',
                      headerName: 'RSD',
                      description: '',
                      headerAlign: 'center',
                      freeReordering: true,
                      children: [{ field: 'cenaUkPDVRSD' }]
                  }
                ]
              },
            ]
          },    ];

    return columnGroupingModel;
}

const listOfColGroupsCombinations = [
    getStanColumnGroupsValutaPDVJM,
    getStanColumnGroupsValutaJMPDV,
    getStanColumnGroupsJMValutaPDV,
    getStanColumnGroupsJMPDVValuta,
    getStanColumnGroupsPDVValutaJM,
    getStanColumnGroupsPDVJMValuta,
];

export function changeGroupingOrder(ind) {
    return listOfColGroupsCombinations[ind]();
}