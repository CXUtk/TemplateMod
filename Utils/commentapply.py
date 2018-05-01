import re
patternStr = r'/// <summary>\n		///(.*)\n		/// </summary>'
patternStr2 = r'[0-9]+\s+([^\s]+)'
pattern = re.compile(patternStr)
pattern2 = re.compile(patternStr2)
listStr = []
replaceStr = []
num = 0

with open('MyDustID.cs', mode='r+') as dustFile:
    content = dustFile.read()
    
    with open('input.txt', mode='r') as inputF:
        content1 = inputF.read()
        matches1 = pattern2.findall(content1)

        def one_by_one(m):
            global num
            ret = matches1[num]
            num += 1
            return '/// <summary>\n		/// ' + ret + '\n		/// </summary>'
        result = re.sub(patternStr, one_by_one, content, len(matches1))
    
    with open('MyDustID_res.cs', mode='w') as output:
        output.write(result)

    print('Finish')
    